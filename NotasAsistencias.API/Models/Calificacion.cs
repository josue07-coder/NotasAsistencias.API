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


        [Range(0, 100)]
        public int Nota { get; set; }

        [NotMapped]
        public string Literal => Nota >= 90 ? "A" :
                                 Nota >= 80 ? "B" :
                                 Nota >= 70 ? "C" : "F";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación
        public Estudiante Estudiante { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public int DocenteId { get; set; }
        public Docente Docente { get; set; }    
    }
}
