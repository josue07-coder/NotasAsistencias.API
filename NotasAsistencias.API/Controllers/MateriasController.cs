using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Data;
using NotasAsistencias.API.Models;
using NotasAsistencias.API.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasAsistencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("estado")]
        public async Task<ActionResult<IEnumerable<MateriaDtos>>> ObtenerMateriasPorEstado([FromQuery] string estado = "Activas")
        {
            Console.WriteLine($"[DEBUG] Estado recibido: {estado}");

            var query = _context.Materias.AsQueryable();

            switch (estado.ToLower())
            {
                case "activas":
                    query = query.Where(m => m.Activa == true);
                    break;
                case "inactivas":
                    query = query.Where(m => m.Activa == false);
                    break;
                case "todas":
                default:
                    // No filtro, las devuelve todas
                    break;
            }

            var resultado = await query
                .Select(m => new MateriaDtos
                {
                    MateriaId = m.MateriaId,
                    Nombre = m.Nombre,
                    Activa = m.Activa
                })
                .ToListAsync();

            Console.WriteLine($"[DEBUG] Materias encontradas: {resultado.Count}");

            return Ok(resultado);
        }

        [HttpGet("activas")]
        public async Task<IActionResult> ObtenerMateriasActivas()
        {
            var materias = await _context.Materias
                .Where(m => m.Activa) // O como valides que estén activas
                .Select(m => new MateriaDtos
                {
                    MateriaId = m.MateriaId,
                    Nombre = m.Nombre
                }).ToListAsync();

            return Ok(materias);
        }

        [HttpGet("{id}/detalles")]
        public async Task<IActionResult> ObtenerDetalleMateria(int id)
        {
            var materia = await _context.Materias
                .Include(m => m.Estudiantes)
                    .ThenInclude(me => me.Estudiante)
                .Include(m => m.DocentesAsignados)
                    .ThenInclude(md => md.Docente)
                .FirstOrDefaultAsync(m => m.MateriaId == id);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            var resultado = new MateriaDetalleDto
            {
                DocenteId = materia.DocentesAsignados.FirstOrDefault()?.DocenteId ?? 0,
                Docente = materia.DocentesAsignados.FirstOrDefault()?.Docente?.NombreCompleto,
                Estudiantes = materia.Estudiantes.Select(e => new EstudianteDtos
                {
                    EstudianteId = e.EstudianteId,
                    NombreCompleto = e.Estudiante.NombreCompleto
                }).ToList()
            };

            return Ok(resultado);
        }

        [HttpGet("{id}/estudiantes")]
        public async Task<IActionResult> ObtenerEstudiantesAsignados(int id)
        {
            var estudiantes = await _context.MateriasEstudiantes
                .Where(me => me.MateriaId == id)
                .Include(me => me.Estudiante)
                .Select(me => new EstudianteAsignadoDto
                {
                    EstudianteId = me.Estudiante.EstudianteId,
                    NombreCompleto = me.Estudiante.NombreCompleto,
                    Matricula = me.Estudiante.Matricula,
                    Correo = me.Estudiante.Correo,
                    Telefono = me.Estudiante.Telefono
                })
                .ToListAsync();

            return Ok(estudiantes);
        }




        // GET: api/Materias/estudiantes?activo=true
        [HttpGet("estudiantes")]
        public async Task<ActionResult<IEnumerable<EstudianteDtos>>> ObtenerEstudiantes([FromQuery] bool? activo = null)
        {
            var query = _context.Estudiantes.AsQueryable();

            if (activo.HasValue)
                query = query.Where(e => e.Activo == activo.Value);

            var estudiantes = await query
                .Select(e => new EstudianteDtos
                {
                    EstudianteId = e.EstudianteId,
                    NombreCompleto = e.NombreCompleto
                })
                .ToListAsync();

            return Ok(estudiantes);
        }


        // GET: api/Materias/docentes
        [HttpGet("docentes")]
        public async Task<ActionResult<IEnumerable<DocenteDtos>>> ObtenerDocentes()
        {
            var docentes = await _context.Docentes
                .Where(d => d.Estado)
                .Select(d => new DocenteDtos
                {
                    DocenteId = d.DocenteId,
                    NombreCompleto = d.NombreCompleto
                })
                .ToListAsync();

            return Ok(docentes);
        }

        // POST: api/Materias
        [HttpPost]
        public async Task<ActionResult> CrearMateria([FromBody] Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return Ok("Materia registrada correctamente.");
        }

        // PUT: api/Materias/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> EditarMateria(int id, [FromBody] Materia materia)
        {
            if (id != materia.MateriaId)
                return BadRequest("El ID no coincide con la materia.");

            _context.Entry(materia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Materia actualizada correctamente.");
        }

        // DELETE: api/Materias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesactivarMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
                return NotFound("Materia no encontrada.");

            if (!materia.Activa)
                return BadRequest("La materia ya está desactivada.");

            materia.Activa = false;
            await _context.SaveChangesAsync();

            return Ok("Materia desactivada correctamente.");
        }

        // POST: api/Materias/{materiaId}/asignar-estudiantes
        [HttpPost("{materiaId}/asignar-estudiantes")]
        public async Task<IActionResult> AsignarEstudiantes(int materiaId, [FromBody] List<int> idsEstudiantes)
        {
            var materia = await _context.Materias
                .Include(m => m.Estudiantes)
                .FirstOrDefaultAsync(m => m.MateriaId == materiaId);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            foreach (var id in idsEstudiantes)
            {
                if (!materia.Estudiantes.Any(me => me.EstudianteId == id))
                {
                    materia.Estudiantes.Add(new MateriaEstudiante { MateriaId = materiaId, EstudianteId = id });
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Estudiantes asignados correctamente.");
        }

        // POST: api/Materias/{materiaId}/asignar-docentes
        [HttpPost("{materiaId}/asignar-docentes")]
        public async Task<IActionResult> AsignarDocentes(int materiaId, [FromBody] List<int> idsDocentes)
        {
            var materia = await _context.Materias
                .Include(m => m.DocentesAsignados)
                .FirstOrDefaultAsync(m => m.MateriaId == materiaId);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            foreach (var id in idsDocentes)
            {
                if (!materia.DocentesAsignados.Any(dm => dm.DocenteId == id))
                {
                    materia.DocentesAsignados.Add(new DocenteMateria { MateriaId = materiaId, DocenteId = id });
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Docentes asignados correctamente.");
        }
    }
}
