using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Dtos
{
    public class AsistenciaDto
    {
        public int EstudianteId { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoAsistencia Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}
