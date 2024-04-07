using FileSharing.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace FileSharing.Api
{
    public class FileSharingDbContext : DbContext
    {
        public FileSharingDbContext(DbContextOptions<FileSharingDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Chunk> Chunks { get; set; }
        public DbSet<FileSharing.Api.Models.FileRecord> Files { get; set; }
        public DbSet<MetaData> MetaDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileRecord>()
                .HasKey(fr => fr.FileRecordId);
                
            modelBuilder.Entity<FileRecord>()
                .HasOne(e => e.MetaData)
                .WithOne(e => e.File)
                .HasForeignKey<MetaData>(e => e.MetaDataId)
                .IsRequired();
        }


    }
}