namespace NotaAsistencia.Client.Models
{
    public class EstudianteModel
    {
        public int EstudianteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; } = true;
    }
}
