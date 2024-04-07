using System.ComponentModel.DataAnnotations;

namespace FileSharing.Api.Models
{
    public class MetaData
    {
        public int MetaDataId { get; set; }
        [Required]
        public int FileSize { get; set; }
        public string FileName { get; set; }
        [Required]
        public string FileType { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public virtual FileSharing.Api.Models.FileRecord File { get; set; }
        public string PreviewURL { get; set; }

    }
}