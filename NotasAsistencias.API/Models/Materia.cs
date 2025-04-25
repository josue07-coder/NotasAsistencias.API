using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        public bool Activa { get; set; } = true;

        // Relaciones
        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<DocenteMateria> DocentesAsignados { get; set; } = new List<DocenteMateria>();
        public ICollection<MateriaEstudiante> Estudiantes { get; set; } = new List<MateriaEstudiante>();

        // Campos opcionales para expandir en el futuro:
        // public string Descripcion { get; set; } = string.Empty;
        // public bool Activa { get; set; } = true;

    }
}
