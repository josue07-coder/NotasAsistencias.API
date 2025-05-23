﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Data;
using NotasAsistencias.API.Dtos;
using NotasAsistencias.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NotasAsistencias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstudiantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estudiantes/estado?activo=true
        [HttpGet("estado")]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetPorEstado([FromQuery] bool activo)
        {
            return await _context.Estudiantes
                                 .Where(e => e.Activo == activo)
                                 .ToListAsync();
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return NotFound();

            return estudiante;
        }

        // POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante([FromBody] EstudianteCreateDto dto)
        {
            var estudiante = new Estudiante
            {
                NombreCompleto = dto.NombreCompleto,
                Matricula = dto.Matricula,
                FechaNacimiento = dto.FechaNacimiento,
                Direccion = dto.Direccion,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            var error = await ValidarEstudianteAsync(estudiante);
            if (error != null)
                return BadRequest(error);

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.EstudianteId }, estudiante);
        }

        // PUT: api/Estudiantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, EstudianteUpdateDto dto)
        {
            if (id != dto.EstudianteId)
                return BadRequest("El ID enviado no coincide con el estudiante.");

            var estudiante = new Estudiante
            {
                EstudianteId = dto.EstudianteId,
                NombreCompleto = dto.NombreCompleto,
                Matricula = dto.Matricula,
                FechaNacimiento = dto.FechaNacimiento,
                Direccion = dto.Direccion,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                FechaRegistro = DateTime.Now
            };

            var error = await ValidarEstudianteAsync(estudiante, id);
            if (error != null)
                return BadRequest(error);

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Estudiante actualizado correctamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                    return NotFound("Estudiante no encontrado.");

                throw;
            }
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return NotFound("Estudiante no encontrado.");

            if (!estudiante.Activo)
                return BadRequest("El estudiante ya está inactivo.");

            estudiante.Activo = false;
            await _context.SaveChangesAsync();

            return Ok("Estudiante desactivado correctamente.");
        }

        // Helpers
        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.EstudianteId == id);
        }

        private async Task<string?> ValidarEstudianteAsync(Estudiante estudiante, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(estudiante.NombreCompleto) ||
                !Regex.IsMatch(estudiante.NombreCompleto, "^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$"))
            {
                return "El nombre es obligatorio y no debe contener números.";
            }

            var hoy = DateTime.Today;
            var edad = hoy.Year - estudiante.FechaNacimiento.Year;
            if (estudiante.FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            if (edad < 3)
                return "El estudiante debe tener al menos 3 años.";

            if (!string.IsNullOrWhiteSpace(estudiante.Matricula))
            {
                bool duplicada = await _context.Estudiantes
                    .AnyAsync(e => e.Matricula == estudiante.Matricula && e.EstudianteId != id);
                if (duplicada)
                    return "Ya existe otro estudiante con esa matrícula.";
            }

            if (!string.IsNullOrWhiteSpace(estudiante.Correo))
            {
                if (!new EmailAddressAttribute().IsValid(estudiante.Correo))
                    return "El correo electrónico no es válido.";

                bool correoUsado = await _context.Estudiantes
                    .AnyAsync(e => e.Correo == estudiante.Correo && e.EstudianteId != id);
                if (correoUsado)
                    return "Este correo ya está registrado por otro estudiante.";
            }

            if (!string.IsNullOrWhiteSpace(estudiante.Telefono) &&
                !Regex.IsMatch(estudiante.Telefono, "^[0-9]+$"))
            {
                return "El teléfono solo debe contener números.";
            }

            return null;
        }
    }
}
