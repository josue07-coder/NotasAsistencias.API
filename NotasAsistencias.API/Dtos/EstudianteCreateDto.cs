namespace NotasAsistencias.API.Dtos
{
    public class EstudianteCreateDto
    {
        public string NombreCompleto { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
