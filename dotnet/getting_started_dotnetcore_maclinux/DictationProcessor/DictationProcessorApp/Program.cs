using System;
using System.IO;
using System.Threading.Tasks;

namespace DictationProcessorApp
{
    class Program
    {
        static void Main(string[] args)
        {
             Parallel.ForEach(
                Directory.GetDirectories("../uploads"),
                subFolder =>
            {
            });
        }
    }
}
