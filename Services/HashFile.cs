namespace FileSharing.Api.Services
{
    public class HashFile
    {
        // This function check if the two chunks are different or identical
        public static bool IsDifferentChunk(byte[] chunk1, byte[] chunk2) {
            return chunk1.GetHashCode() != chunk2.GetHashCode();
        }
    }
}