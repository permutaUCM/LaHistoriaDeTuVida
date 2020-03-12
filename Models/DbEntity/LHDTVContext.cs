using Microsoft.EntityFrameworkCore;

namespace LHDTV.Models.DbEntity
{
    public class LHDTVContext : DbContext
    {
        public LHDTVContext ()
            : base()
        {
        }

        public DbSet<PhotoDb> Photo { get; set; }
        public DbSet<TagDb> TagDb { get; set; }
        public DbSet<UserDb> Usuario { get; set; }

        public DbSet<FolderDb> Folder {get;set;}

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=LHDTV;Trusted_Connection=True;", providerOptions => providerOptions.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }

        public void Update(FolderDb EntFolder ,PhotoDb photo){


            

        }


    }
}