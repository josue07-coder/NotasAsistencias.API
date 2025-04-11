namespace NotaAsistencia.Client.Models
{
    public class CalificacionModel
    {
        public int CalificacionId { get; set; }
        public int EstudianteId { get; set; }
        public string Materia { get; set; } = string.Empty;
        public int Nota { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
