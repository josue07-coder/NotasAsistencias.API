using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Dtos
{
    public class AsistenciaConEstudianteDto
    {
        public int AsistenciaId { get; set; }
        public int EstudianteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public EstadoAsistencia Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}
