using FileSharing.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace FileSharing.Api
{
    public class FileSharingDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database =< myMssqlDB >; User Id = sa; Password = reallyStrongPassword_123; TrustServerCertificate = True; Encrypt = false");
            
        }

    }
}