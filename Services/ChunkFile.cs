namespace FileSharingSystem.Api.Services
{
    public class ChunkFile
    {
        private string FilePathName { get; set; }

        private int ChunkSize { get; set; } // 1 Megabyte multiplied by the number of MB needed

        public ChunkFile(string filePathName, int size = 1048576 * 4)
        {
            this.FilePathName = filePathName;
            this.ChunkSize = size;
        }

        public List<byte[]> ChunkData()
        {
            try
            {
            var dataInBytes = File.ReadAllBytes(this.FilePathName);
            var chunks = new List<byte[]>();
            var chunk = new byte[ChunkSize];
            var chunkIndex = 0;
            foreach (var byteValues in dataInBytes)
            {
               chunk[chunkIndex++] = byteValues;
               if (chunkIndex == ChunkSize) {
                chunks.Add(chunk);
                chunk = new byte[ChunkSize];
                chunkIndex = 0;
               }
                
            }
            if (chunkIndex != 0) {
                var lastChunk = new byte[chunkIndex];
                Array.Copy(chunk, lastChunk, chunkIndex);
                chunks.Add(lastChunk);
            }

            return chunks;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("CAN'T OPEN FILE ", e.Message);
                throw;
            }
            
        }
    }
}
