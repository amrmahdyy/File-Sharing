using FileSharing.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileSharing.Api.Controllers
{
    [Route("api/[controller]")]
    public class ChunkController: ControllerBase
    {
        private readonly FileSharingDbContext context;
        public ChunkController(FileSharingDbContext context) {
            this.context = context;
        }
        public IActionResult Create([FromBody] Chunk chunk) {
            // context.Chunks.Add()
            
            return Created($"/api/chunck/{chunk.id}", chunk);
        }
    }
}