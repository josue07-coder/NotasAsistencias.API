using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Models
{
    public class Calificacion
    {
        [Key]
        public int CalificacionId { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Materia { get; set; }  // Fija: Lengua, Matemática, etc.

        [Range(0, 100)]
        public int Nota { get; set; }

        [NotMapped]
        public string Literal => Nota >= 90 ? "A" :
                                 Nota >= 80 ? "B" :
                                 Nota >= 70 ? "C" : "F";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación
        public Estudiante Estudiante { get; set; }
    }
}
