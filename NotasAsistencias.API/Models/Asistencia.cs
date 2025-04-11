using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Models
{
    public enum EstadoAsistencia
    {
        Presente,
        Ausente,
        Excusa
    }
    public class Asistencia
    {
        [Key]
        public int AsistenciaId { get; set; }

        [Required]
        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public EstadoAsistencia Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}
