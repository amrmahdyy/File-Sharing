namespace FileSharing.Api.Models
{
    public class MetaData
    {
        public int Id { get; set; }
        public int FileSize { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string OwnerId { get; set; }
        public virtual FileSharing.Api.Models.File File { get; set; }
        public string PreviewURL { get; set; }

    }
}