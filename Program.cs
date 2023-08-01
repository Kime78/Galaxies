using System.Diagnostics;
using ServerJarsAPI;
using ServerJarsAPI.Events;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


if (!Directory.Exists("Instances"))
{
    Directory.CreateDirectory("Instances");
}


//download minecraft server jar
var serversAPI = new ServerJars();
using var fileStream1 = File.Create("./forge.jar");
Progress<ProgressEventArgs> progress = new();
progress.ProgressChanged += (_, e) =>
{
    Console.Write($"\rProgress: {e.ProgressPercentage}% ({e.BytesTransferred / 1024 / 1024}MB / {e.TotalBytes / 1024 / 1024}MB)          ");
};
await serversAPI.GetJar(fileStream1, "modded", "forge", progress: progress);
await fileStream1.FlushAsync();
Console.WriteLine($"\nDownloaded {fileStream1.Length / 1024 / 1024}MB to {fileStream1.Name}");

//execute the server command command
Process p = new Process();
p.StartInfo.UseShellExecute = false;
p.StartInfo.FileName = "java";
p.StartInfo.Arguments = "-Xmx1024M -Xms1024M -jar server1.jar nogui";
p.Start();

app.MapGet("/", () => "Hello World!");

app.Run();
