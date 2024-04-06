using FileSharing.Api.Models;

namespace FileSharing.Api.Models
{
    public class File
    {
        public int FileId { get; set; }

        public ICollection<Chunk> Chunks { get; set; }
        
        public MetaData MetaData { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}