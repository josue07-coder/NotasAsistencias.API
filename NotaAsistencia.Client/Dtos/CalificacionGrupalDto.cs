namespace NotaAsistencia.Client.Dtos
{
    public class CalificacionGrupalDto
    {
        public int MateriaId { get; set; }
        public int DocenteId { get; set; }
        public List<EstudianteNotaDto> Calificaciones { get; set; } = new();
    }
}
