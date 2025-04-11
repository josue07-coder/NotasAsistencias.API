using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Data;
using NotasAsistencias.API.Dtos;
using NotasAsistencias.API.Models;
using NotasAsistencias.API.Dtos;

namespace NotasAsistencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstudiantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estudiantes
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
            {
                return NotFound();
            }

            return estudiante;
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                FechaRegistro = DateTime.Now // o mantenlo si lo quieres actualizar
            };

            var error = await ValidarEstudianteAsync(estudiante, estudiante.EstudianteId);
            if (error != null)
                return BadRequest(error);

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estudiantes.Any(e => e.EstudianteId == id))
                    return NotFound("Estudiante no encontrado.");

                throw;
            }

            return Ok("Estudiante actualizado correctamente.");
        }


        // POST: api/Estudiantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

            return Ok("Estudiante desactivado Eliminado Corrextamente.");
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.EstudianteId == id);
        }

        private async Task<string?> ValidarEstudianteAsync(Estudiante estudiante, int? id = null)
        {
            // Nombre requerido y sin números
            if (string.IsNullOrWhiteSpace(estudiante.NombreCompleto) ||
                !Regex.IsMatch(estudiante.NombreCompleto, "^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$"))
            {
                return "El nombre es obligatorio y no debe contener números.";
            }

            // Edad mínima de 3 años
            var hoy = DateTime.Today;
            var edad = hoy.Year - estudiante.FechaNacimiento.Year;
            if (estudiante.FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            if (edad < 3)
            {
                return "El estudiante debe tener al menos 3 años.";
            }

            // Matrícula duplicada (excluyendo el mismo ID si es PUT)
            if (!string.IsNullOrWhiteSpace(estudiante.Matricula))
            {
                bool duplicada = await _context.Estudiantes
                    .AnyAsync(e => e.Matricula == estudiante.Matricula && e.EstudianteId != id);
                if (duplicada)
                    return "Ya existe otro estudiante con esa matrícula.";
            }

            // Correo válido y único
            if (!string.IsNullOrWhiteSpace(estudiante.Correo))
            {
                if (!new EmailAddressAttribute().IsValid(estudiante.Correo))
                    return "El correo electrónico no es válido.";

                bool correoUsado = await _context.Estudiantes
                    .AnyAsync(e => e.Correo == estudiante.Correo && e.EstudianteId != id);
                if (correoUsado)
                    return "Este correo ya está registrado por otro estudiante.";
            }

            // Teléfono solo números
            if (!string.IsNullOrWhiteSpace(estudiante.Telefono) &&
                !Regex.IsMatch(estudiante.Telefono, "^[0-9]+$"))
            {
                return "El teléfono solo debe contener números.";
            }

            return null; // Si todo está bien
        }
    }
}
