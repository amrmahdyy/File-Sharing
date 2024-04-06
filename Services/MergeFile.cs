namespace FileSharing.Api.Services
{
    public class MergeFile
    {
        private List<byte[]> Chunks { get; set; }

        public MergeFile(List<byte[]> Chunks) {
            this.Chunks = Chunks;
        }
        public List<Byte> GetCompleteFileInBytes() {
            var fileInBytes = new List<Byte>();
            foreach (var chunk in Chunks)
            {
                foreach (var b in chunk)
                {
                    fileInBytes.Add(b);
                }
                
            }
            return fileInBytes;
        }
        public void SaveFile(string filePath, List<Byte> data) {
            try
            {
            File.WriteAllBytes(filePath, data.ToArray());
            System.Console.WriteLine("Data Written successfully to ", filePath);
                
            }
            catch (System.Exception ex)
            {
             System.Console.WriteLine("Failed to write data ", ex.Message);   
                throw;
            }
        }

    }
}