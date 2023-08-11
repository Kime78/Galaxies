using Microsoft.AspNetCore.Mvc;
using Galaxies.Models;
using Microsoft.Extensions.ObjectPool;

namespace Galaxies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InstanceController : Controller
    {
        [HttpGet]
        public JsonResult GetInstances()
        {
            var directories = Directory.GetDirectories("./Instances");
            List<Instance> instances = new List<Instance>();   
            foreach (var directory in directories) {
                Instance? inst = Instance.loadInstanceFromConfig(directory);
                if(inst != null)
                {
                    instances.Add(inst);   
                }
            }
            return Json(instances);
        }

        [HttpPost]
        public void createInstance(Instance instance, string path)
        {
            Directory.CreateDirectory(path);
            instance.createConfigFile(path);
        }
    }
}