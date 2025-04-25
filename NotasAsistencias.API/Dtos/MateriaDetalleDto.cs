namespace NotasAsistencias.API.Dtos
{
    public class MateriaDetalleDto
    {
        public int DocenteId { get; set; }
        public string? Docente { get; set; }
        public List<EstudianteDtos> Estudiantes { get; set; } = new();
    }
}
