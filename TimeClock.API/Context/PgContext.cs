using Microsoft.EntityFrameworkCore;
using TimeClock.API.Entities;

namespace TimeClock.API.Context
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> opt) : base(opt)
        {
        }

        public DbSet<Entities.TimeClock> TimeClocks {  get; set; }
    }
}
