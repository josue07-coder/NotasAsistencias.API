using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }

        [Required, StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true; // Activo por defecto

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaContratacion { get; set; }

        [NotMapped]
        public int Edad => DateTime.Today.Year - FechaNacimiento.Year -
                          (FechaNacimiento.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - FechaNacimiento.Year)) ? 1 : 0);

        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public ICollection<DocenteMateria> MateriasAsignadas { get; set; } = new List<DocenteMateria>();
    }
}
