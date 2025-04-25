namespace NotasAsistencias.API.Dtos
{
    public class CalificacionGrupalDto
    {
        public int MateriaId { get; set; }
        public int DocenteId { get; set; }
        public List<EstudianteNotaDto> Calificaciones { get; set; } = new();
    }
    public class EstudianteNotaDto
    {
        public int EstudianteId { get; set; }
        public int Nota { get; set; }
    }
}
