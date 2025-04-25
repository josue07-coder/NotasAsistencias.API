using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Estudiante Estudiante { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public EstadoAsistencia Estado { get; set; }

        public string Observaciones { get; set; } = string.Empty;

        [Required]
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        [Required]
        public int DocenteId { get; set; }
        public Docente Docente { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
