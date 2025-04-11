namespace NotasAsistencias.API.Dtos
{
    public class CalificacionConEstudianteDto
    {
        public int CalificacionId { get; set; }
        public int EstudianteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Materia { get; set; } = string.Empty;
        public int Nota { get; set; }
        public string Literal { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
