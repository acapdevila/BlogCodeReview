using Domain.Model;
using System.Data.Entity;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
      
    }
}
