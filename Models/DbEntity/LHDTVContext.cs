using Microsoft.EntityFrameworkCore;

namespace LHDTV.Models.DbEntity
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<PhotoDb> Photo { get; set; }
        public DbSet<TagDb> TagDb { get; set; }
        public DbSet<UserDb> User { get; set; }
    }
}