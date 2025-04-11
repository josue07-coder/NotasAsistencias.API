using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Data;
using NotasAsistencias.API.Dtos;
using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalificacionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Calificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetCalificaciones()
        {
            return await _context.Calificaciones.ToListAsync();
        }

        // GET: api/Calificacions/5
        [HttpGet("detalles")]
        public async Task<ActionResult<IEnumerable<CalificacionDetalleDto>>> GetDetalles()
        {
            var datos = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Select(c => new CalificacionDetalleDto
                {
                    CalificacionId = c.CalificacionId,
                    EstudianteId = c.EstudianteId,
                    NombreEstudiante = c.Estudiante.NombreCompleto,
                    Materia = c.Materia,
                    Nota = c.Nota,
                    FechaRegistro = c.FechaRegistro
                }).ToListAsync();

            return Ok(datos);
        }
        // GET: api/Calificaciones/con-estudiante
        [HttpGet("con-estudiante")]
        public async Task<ActionResult<IEnumerable<CalificacionConEstudianteDto>>> GetConEstudiante()
        {
            var datos = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Select(c => new CalificacionConEstudianteDto
                {
                    CalificacionId = c.CalificacionId,
                    EstudianteId = c.EstudianteId,
                    NombreCompleto = c.Estudiante.NombreCompleto,
                    Matricula = c.Estudiante.Matricula,
                    Materia = c.Materia,
                    Nota = c.Nota,
                    Literal = c.Literal,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync();

            return Ok(datos);
        }


        // PUT: api/Calificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Calificaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacion(int id, [FromBody] CalificacionCreateDto dto)
        {
            // Validación de ID (Asegúrate de tener este valor correctamente en el frontend)
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
                return NotFound("No se encontró la calificación.");

            calificacion.Nota = dto.Nota;

            var error = await ValidarCalificacionAsync(calificacion, id);
            if (error != null)
                return BadRequest(error);

            _context.Entry(calificacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Calificación actualizada correctamente.");
        }


        // POST: api/Calificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCalificacion(CalificacionCreateDto dto)
        {
            var calificacion = new Calificacion
            {
                EstudianteId = dto.EstudianteId,
                Materia = dto.Materia,
                Nota = dto.Nota,
                FechaRegistro = DateTime.Now
            };

            var error = await ValidarCalificacionAsync(calificacion);
            if (error != null)
                return BadRequest(new { mensaje = error });

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();

            return Ok("Calificación registrada");
        }


        // DELETE: api/Calificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacion(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificaciones.Any(e => e.CalificacionId == id);
        }

        private async Task<string?> ValidarCalificacionAsync(Calificacion calificacion, int? id = null)
        {
            // Validar estudiante
            var estudiante = await _context.Estudiantes.FindAsync(calificacion.EstudianteId);
            if (estudiante == null)
                return "El estudiante no existe.";

            if (!estudiante.Activo)
                return "El estudiante está inactivo.";

            // Validar materia
            var materiasValidas = new[] { "Lengua española", "Matemáticas", "Ciencias sociales", "Ciencias naturales" };
            if (!materiasValidas.Contains(calificacion.Materia))
                return "Materia no válida. Solo se permiten las 4 materias básicas.";

            // Validar nota
            if (calificacion.Nota < 0 || calificacion.Nota > 100)
                return "La nota debe estar entre 0 y 100.";

            // Validar duplicado por estudiante y materia
            bool yaExiste = await _context.Calificaciones
                .AnyAsync(c => c.EstudianteId == calificacion.EstudianteId
                            && c.Materia == calificacion.Materia
                            && c.CalificacionId != id);

            if (yaExiste)
                return "Ya existe una calificación para este estudiante en esta materia.";

            return null;
        }
    }
}
