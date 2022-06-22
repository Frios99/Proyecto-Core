namespace MVCCore_Francis.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Rol { get; set; }
        public int Contrasena { get; set; }
        public int? NivelTranstorno { get; set; }
    }
}
