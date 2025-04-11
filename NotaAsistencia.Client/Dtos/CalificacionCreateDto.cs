using System.ComponentModel.DataAnnotations;

namespace NotaAsistencia.Client.Dtos
{
    public class CalificacionCreateDto
    {
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "La materia es obligatoria.")]
        public string Materia { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "La nota debe estar entre 0 y 100.")]
        public int Nota { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
