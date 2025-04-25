using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Dtos
{
    public class AsignarDocentesDto
    {
        [Required]
        public int MateriaId { get; set; }

        [Required]
        public List<int> DocentesIds { get; set; } = new();
    }
}
