namespace NotaAsistencia.Client.Models
{
    public class DocenteEdit
    {
        public int DocenteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaContratacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }

        public int Edad =>
            DateTime.Today.Year - FechaNacimiento.Year -
            (FechaNacimiento.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - FechaNacimiento.Year)) ? 1 : 0);
    }
}
