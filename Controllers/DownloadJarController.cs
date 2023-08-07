using Microsoft.AspNetCore.Mvc;
using ServerJarsAPI;
using Galaxies.Models;

namespace Galaxies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DownloadJarController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> download(String path, Jar jar)
        {
            var serversAPI = new ServerJars();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using var stream = System.IO.File.Create(path + "/server.jar");
            
            await serversAPI.GetJar(stream, jar.type, jar.category, jar.version);
            return Ok();
        }
    }
}