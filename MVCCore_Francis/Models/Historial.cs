namespace MVCCore_Francis.Models
{
    public class Historial
    {
        public int HistorialID { get; set; }

        public DateTime Fecha { get; set; }
        public string? Historia { get; set; }
        public string? Titulo { get; set; }
        public int UsuarioID { get; set; }
    }
}
