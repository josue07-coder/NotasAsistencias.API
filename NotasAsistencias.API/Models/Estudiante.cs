using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Models
{
    public class Estudiante
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Matricula { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        public int Edad => DateTime.Today.Year - FechaNacimiento.Year -
                          (FechaNacimiento.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - FechaNacimiento.Year)) ? 1 : 0);

        [StringLength(200)]
        public string? Direccion { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string? Correo { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relaciones
        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<MateriaEstudiante> Materias { get; set; } = new List<MateriaEstudiante>();

    }
}
