using System;
using System.IO;
using System.Threading.Tasks;
using DictationProcessorLib;

namespace DictationProcessorApp
{
    class Program
    {
        static void Main(string[] args)
        {
             Parallel.ForEach(
                Directory.GetDirectories("../../uploads"),
                subFolder =>
            {
                var uploadProcessor = new UploadProcessor(subFolder);
                uploadProcessor.Process();
            });
        }
    }
}
