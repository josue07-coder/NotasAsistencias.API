namespace NotaAsistencia.Client.Dtos
{
    public class AsignacionDocentesDto
    {
        public int MateriaId { get; set; }
        public List<int> DocentesIds { get; set; } = new();
    }
}
