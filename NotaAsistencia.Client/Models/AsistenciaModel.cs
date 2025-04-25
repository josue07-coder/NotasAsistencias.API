namespace NotaAsistencia.Client.Models
{
    public class AsistenciaModel
    {
        public int EstudianteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public EstadoAsistencia Estado { get; set; } = EstadoAsistencia.Presente;
        public string? Observaciones { get; set; }
    }
    public enum EstadoAsistencia
    {
        Presente,
        Ausente,
        Excusa
    }
}
