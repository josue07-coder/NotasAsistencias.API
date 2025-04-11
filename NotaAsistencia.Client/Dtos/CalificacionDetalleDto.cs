namespace NotaAsistencia.Client.Dtos
{
    public class CalificacionDetalleDto
    {
        public int CalificacionId { get; set; }
        public int EstudianteId { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public string Materia { get; set; } = string.Empty;
        public int Nota { get; set; }
        public string Literal => Nota >= 90 ? "A" :
                                 Nota >= 80 ? "B" :
                                 Nota >= 70 ? "C" : "F";
        public DateTime FechaRegistro { get; set; }
    }
}
