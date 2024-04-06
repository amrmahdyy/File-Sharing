namespace FileSharing.Api.Models
{
    public class Chunk
    {
        public string BlobName { get; set; }
        public virtual MetaData MetaData  { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}