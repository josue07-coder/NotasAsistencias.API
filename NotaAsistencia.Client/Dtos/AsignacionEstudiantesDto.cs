namespace NotaAsistencia.Client.Dtos
{
    public class AsignacionEstudiantesDto
    {
        public int MateriaId { get; set; }
        public List<int> EstudiantesIds { get; set; } = new();
    }
}
