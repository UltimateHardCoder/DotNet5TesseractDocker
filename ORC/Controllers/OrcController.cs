namespace ORC.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Helper;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ORCController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> DoOCR()
        {
            var results = new List<string>();
            if (!HttpContext.Request.HasFormContentType)
                return Ok(results);

            var currentDirectory = Directory.GetCurrentDirectory();

            foreach (var file in HttpContext.Request.Form.Files)
                results.Add(await OrcReader.ReadAsync(file, currentDirectory));

            return Ok(results);
        }
    }
}