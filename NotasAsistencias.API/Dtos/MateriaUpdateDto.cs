using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Dtos
{
    public class MateriaUpdateDto
    {
        [Required]
        public int MateriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}
