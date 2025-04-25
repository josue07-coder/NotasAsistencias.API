namespace NotaAsistencia.Client.Dtos
{
    public class DocenteCreateDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public string FechaContratacion { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string FechaIngreso { get; set; } = string.Empty;
    }
}
