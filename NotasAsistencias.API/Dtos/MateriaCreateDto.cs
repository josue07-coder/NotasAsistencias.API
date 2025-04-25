using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Dtos
{
    public class MateriaCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}
