using System.IO;
using IniParser;
using IniParser.Model;

namespace Galaxies.Models
{
    public class Instance
    {
        public int JavaVersion { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = ""; //modded, vanilla, etc.
        public string Type { get; set; } = ""; //forge, fabric, paper, etc.
        public int MinimumRAM { get; set; } // in MibiBytes
        public int MaximumRAM { get; set; } // in MibiBytes
        public string MinecraftVersion { get; set; } = "";
        public string Path { get; set; } = "";

        public static Instance? loadInstanceFromConfig(string instance_path)
        {
            if (System.IO.File.Exists(instance_path + "/config.ini"))
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(instance_path + "/config.ini");
                var instance = new Instance();

                instance.JavaVersion = int.Parse(data["t"]["JavaVersion"]);
                instance.Name = data["t"]["Name"];
                instance.Category = data["t"]["Category"];
                instance.Type = data["t"]["Type"];
                instance.MinimumRAM = int.Parse(data["t"]["MinimumRAM"]);
                instance.MaximumRAM = int.Parse(data["t"]["MaximumRAM"]);
                instance.MinecraftVersion = data["t"]["MinecraftVersion"];
                instance.Path = data["t"]["Path"];

                return instance;
            }
            else
            {
                return null;
            }
        }

        public void createConfigFile(string path)
        {
            var parser = new FileIniDataParser();
            var file = File.Create(path + "/config.ini");
            file.Close();

            IniData data = new IniData();
            
            data.Sections.AddSection("t");
            data["t"]["JavaVersion"] = this.JavaVersion.ToString();
            data["t"]["Name"] = this.Name;
            data["t"]["Category"] = this.Category;
            data["t"]["Type"] = this.Type;
            data["t"]["MinimumRAM"] = this.MinimumRAM.ToString();
            data["t"]["MaximumRAM"] = this.MaximumRAM.ToString();
            data["t"]["MinecraftVersion"] = this.MinecraftVersion;
            data["t"]["Path"] = path;

            parser.WriteFile(path + "/config.ini", data);
        }
    }
}