using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSharing.Api.Controllers
{
    [Route("api/[Controller]")]
    public class FileController : ControllerBase
    {
        private readonly FileSharingDbContext context;
        public FileController(FileSharingDbContext context)
        {
            this.context = context;
        }
        // public ActionResult GetChunksFromCloud() {

        // }
        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] FileSharing.Api.Models.FileRecord file)
        {
            context.Files.Add(file);
            context.SaveChanges();
            return Created($"/api/file/{file.FileRecordId}", file);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public ActionResult Get(int id)
        {
            var file = context.Files.Where(f => f.FileRecordId == id).FirstOrDefault();
            if (file == null)
            {
                return NotFound(new { message = $"File with {id} not found" });
            }
            return Ok(file);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var file = context.Files.Where(f => f.FileRecordId == id).FirstOrDefault();
            if (file == null)
            {
                return NotFound(new { message = $"File with {id} not found" });
            }
            var deletedFile = context.Files.Remove(file);
            context.SaveChanges();
            return Ok(deletedFile);
        }


    }
}