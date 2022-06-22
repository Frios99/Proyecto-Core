using Microsoft.EntityFrameworkCore;
using MVCCore_Francis.Models;

namespace MVCCore_Francis.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NivelTrastorno> NivelTrastornos { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }
        public DbSet<Historial> Historiales { get; set; }
        public DbSet<Cuestionario> Cuestionarios { get; set; }
        public DbSet<PalabrasClave> palabrasClaves { get; set; }

    }
    
}
