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
    public class AsistenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AsistenciasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        [HttpGet]
public async Task<ActionResult<IEnumerable<AsistenciaConEstudianteDto>>> GetAsistencias()
{
    var asistencias = await _context.Asistencias
        .Include(a => a.Estudiante)
        .Where(a => a.Estudiante.Activo)
        .OrderByDescending(a => a.Fecha)
        .Select(a => new AsistenciaConEstudianteDto
        {
            AsistenciaId = a.AsistenciaId,
            EstudianteId = a.EstudianteId, //  AÑADE ESTO TAMBIÉN
            NombreCompleto = a.Estudiante.NombreCompleto,
            Fecha = a.Fecha,
            Estado = a.Estado,
            Observaciones = a.Observaciones
        })

        .ToListAsync();

    return Ok(asistencias);
}


        // GET: api/Asistencias/5
        [HttpGet("por-fecha")]
        public async Task<ActionResult<IEnumerable<AsistenciaConEstudianteDto>>> GetPorFecha([FromQuery] DateTime fecha)
        {
            var resultado = await _context.Asistencias
                .Where(a => a.Fecha.Date == fecha.Date)
                .Where(a => a.Fecha.Date == fecha.Date && a.Estudiante.Activo)
                .Include(a => a.Estudiante)
                .Select(a => new AsistenciaConEstudianteDto
                {
                    AsistenciaId = a.AsistenciaId,
                    EstudianteId = a.EstudianteId, //  AÑADE ESTO TAMBIÉN
                    NombreCompleto = a.Estudiante.NombreCompleto,
                    Fecha = a.Fecha,
                    Estado = a.Estado,
                    Observaciones = a.Observaciones
                })

                .ToListAsync();

            return Ok(resultado);
        }


        // PUT: api/Asistencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(int id, [FromBody] AsistenciaUpdateDto dto)
        {
            if (id != dto.AsistenciaId)
                return BadRequest("El ID no coincide con la asistencia.");

            var asistencia = new Asistencia
            {
                AsistenciaId = dto.AsistenciaId,
                EstudianteId = dto.EstudianteId,
                Fecha = dto.Fecha,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones
            };

            var error = await ValidarAsistenciaAsync(asistencia, dto.AsistenciaId);
            if (error != null)
                return BadRequest(error);

            _context.Entry(asistencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Asistencia actualizada correctamente.");
        }


        // POST: api/Asistencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> RegistrarAsistencias([FromBody] List<AsistenciaDto> asistencias)
        {
            foreach (var dto in asistencias)
            {
                var asistencia = new Asistencia
                {
                    EstudianteId = dto.EstudianteId,
                    Fecha = dto.Fecha,
                    Estado = dto.Estado,
                    Observaciones = dto.Observaciones
                };
                var error = await ValidarAsistenciaAsync(asistencia);
                if (error != null)
                    return BadRequest(error);

                _context.Asistencias.Add(asistencia);
            }

            await _context.SaveChangesAsync();
            return Ok("Asistencias registradas correctamente.");
        }


        // DELETE: api/Asistencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.AsistenciaId == id);
        }

        private async Task<string?> ValidarAsistenciaAsync(Asistencia asistencia, int? id = null)
        {
            var estudiante = await _context.Estudiantes.FindAsync(asistencia.EstudianteId);
            if (estudiante == null)
                return "El estudiante no existe.";

            if (!estudiante.Activo)
                return "El estudiante está inactivo.";

            if (asistencia.Fecha > DateTime.Today)
                return "No se puede registrar asistencia para una fecha futura.";

            bool yaExiste = await _context.Asistencias
                .AnyAsync(a => a.EstudianteId == asistencia.EstudianteId
                            && a.Fecha.Date == asistencia.Fecha.Date
                            && a.AsistenciaId != id);

            if (yaExiste)
                return "Ya se ha registrado asistencia para este estudiante en esa fecha.";

            return null;
        }
    }
}
