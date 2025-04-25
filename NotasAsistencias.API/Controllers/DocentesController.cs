using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Data;
using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocentesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes()
        {
            return await _context.Docentes.OrderBy(d => d.NombreCompleto).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Docente>> GetDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            return docente == null ? NotFound() : Ok(docente);
        }

        [HttpPost]
        public async Task<ActionResult> CrearDocente(Docente docente)
        {
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();
            return Ok("Docente creado correctamente.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditarDocente(int id, Docente docente)
        {
            if (id != docente.DocenteId) return BadRequest("El ID no coincide.");

            _context.Entry(docente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Docente actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
                return NotFound("Docente no encontrado.");

            if (!docente.Estado)
                return BadRequest("El docente ya está inactivo.");

            docente.Estado = false;
            await _context.SaveChangesAsync();

            return Ok("Docente desactivado correctamente.");
        }
        [HttpPut("activar/{id}")]
        public async Task<ActionResult> ActivarDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null) return NotFound();

            if (docente.Estado)
                return BadRequest("El docente ya está activo.");

            docente.Estado = true;
            await _context.SaveChangesAsync();

            return Ok("Docente activado.");
        }

    }
}
