using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;

namespace ApiAppLeon.Context
{
    public class PruebaCoreContext : DbContext
    {
        public PruebaCoreContext(DbContextOptions<PruebaCoreContext> options)
            : base(options)
        { 
        }
        public DbSet<PruebaCore> PruebaCore { get; set; } = null!;
    }
}
