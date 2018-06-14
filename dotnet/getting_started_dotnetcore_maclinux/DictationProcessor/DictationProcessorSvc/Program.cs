using System;
using System.IO;
using DictationProcessorLib;

namespace DictationProcessorSvc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting service.");
            string uploadPath = "../../uploads";
            var fileSystemWatcher = new FileSystemWatcher(uploadPath, "metadata.json");
            fileSystemWatcher.IncludeSubdirectories = true;
            while(true)
            {
                Console.WriteLine("Waiting..."); 
                var result = fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
                Console.WriteLine($"New metadata file {result.Name}");
                var fullMetadataFilePath = Path.Combine(uploadPath, result.Name);
                var subFolder = Path.GetDirectoryName(fullMetadataFilePath);
                var processor = new UploadProcessor(subFolder);
                Console.WriteLine("Starting Process.");
                processor.Process();
            }
        }
    }
}
