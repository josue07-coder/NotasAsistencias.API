namespace NotasAsistencias.API.Dtos
{
    public class CalificacionCreateDto
    {
        public int CalificacionId { get; set; }
        public int EstudianteId { get; set; }
        public string Materia { get; set; } = string.Empty;
        public int Nota { get; set; }
    }
}
