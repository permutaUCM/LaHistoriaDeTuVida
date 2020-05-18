using Microsoft.EntityFrameworkCore;

namespace LHDTV.Models.DbEntity
{
    public class LHDTVContext : DbContext
    {
        public LHDTVContext()
            : base()
        {
        }

        public DbSet<PhotoDb> Photo { get; set; }
        public DbSet<TagDb> TagDb { get; set; }
        public DbSet<UserDb> User { get; set; }

        public DbSet<FolderDb> Folder { get; set; }
        public DbSet<PhotoTagsTypes> TagTypeMaster { get; set; }

        public DbSet<PhotoTransition> PhotoTransition { get; set; }
        public DbSet<PhotoFolderMap> PhotoFolderMap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=LHDTV;Trusted_Connection=True;", providerOptions => providerOptions.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoTagsTypes>().HasKey(t => t.Name);
            modelBuilder.Entity<Extra>().HasKey(t => t.Name);

            modelBuilder.Entity<PhotoFolderMap>().HasKey(pf => new {pf.PhotoId, pf.FolderId} );
            
        }



    }
}