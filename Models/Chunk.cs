using System.ComponentModel.DataAnnotations;

namespace FileSharing.Api.Models
{
    public class Chunk
    {
        public int id { get; set; }
        [Required]
        public string BlobName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}