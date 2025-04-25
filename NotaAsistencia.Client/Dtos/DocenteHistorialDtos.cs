using System.ComponentModel.DataAnnotations;

namespace NotaAsistencia.Client.Dtos
{
    public class DocenteHistorialDtos
    {
        
        public int DocenteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true; // Activo por defecto
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaContratacion { get; set; }
        public int Edad => DateTime.Today.Year - FechaNacimiento.Year -
                         (FechaNacimiento.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - FechaNacimiento.Year)) ? 1 : 0);
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
    }
}
