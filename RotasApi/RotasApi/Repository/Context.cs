using Microsoft.EntityFrameworkCore;
using RotasApi.Model;

namespace RotasApi.Repository
{
    public class Context : DbContext
    {
        public DbSet<Rotas> Rotas { get; set; }

        public Context(DbContextOptions<Context> options) :
            base(options)
        {
        }
    }
}
