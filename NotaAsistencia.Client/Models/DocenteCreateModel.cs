namespace NotaAsistencia.Client.Models
{
    public class DocenteCreateModel
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}
