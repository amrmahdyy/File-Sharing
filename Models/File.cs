using System.ComponentModel.DataAnnotations;
using FileSharing.Api.Models;

namespace FileSharing.Api.Models
{
    public class FileRecord
    {
        public int FileRecordId { get; set; }
        public ICollection<Chunk> Chunks { get; set; }
        public MetaData MetaData { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}