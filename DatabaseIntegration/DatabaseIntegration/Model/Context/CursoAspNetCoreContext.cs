using Microsoft.EntityFrameworkCore;

namespace DatabaseIntegration.Model.Context
{
    public class CursoAspNetCoreContext : DbContext
    {
        public CursoAspNetCoreContext()
        {

        }

        public CursoAspNetCoreContext(DbContextOptions<CursoAspNetCoreContext> options) : base(options) { }

        public DbSet<Person> Persons {get; set;}
    }
}
