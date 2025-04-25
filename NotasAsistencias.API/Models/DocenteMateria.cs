namespace NotasAsistencias.API.Models
{
    public class DocenteMateria
    {
        public int DocenteId { get; set; }
        public Docente Docente { get; set; } = null!;

        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;
    }
}
