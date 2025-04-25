using System.ComponentModel.DataAnnotations;

namespace NotasAsistencias.API.Dtos
{
    public class AsignarEstudiantesDto
    {
        [Required]
        public int MateriaId { get; set; }

        [Required]
        public List<int> EstudiantesIds { get; set; } = new();
    }
}
