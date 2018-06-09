using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DictationProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var subFolder in Directory.GetDirectories("../uploads"))
            {
                var metadataFilePath = Path.Combine(subFolder, "metadata.json");
                Console.WriteLine($"Reading {metadataFilePath}");

                var metadataCollection = GetMetadata(metadataFilePath);


                foreach (var metadata in metadataCollection)
                {
                    var audioFilePath = Path.Combine(subFolder, metadata.File.FileName);
                    var md5Checksum = GetChecksum(audioFilePath);

                    if (md5Checksum.Replace("-", "").ToLower() != metadata.File.Md5Checksum)
                    {
                        throw new Exception("Checksum not verified!");
                    }
                    var uniqueId = Guid.NewGuid();
                    metadata.File.FileName = uniqueId + ".WAV";
                    var newPath = Path.Combine("../ready_for_transcription", uniqueId + ".WAV");
                    CreateCompressedFile(audioFilePath, newPath);
                    SaveSingleMetadata(metadata, newPath + ".json");
                }
            }
        }

        static void CreateCompressedFile(string inputFilePath, string outputFilePath)
        {
            outputFilePath += ".gz";
            Console.WriteLine($"Creating {outputFilePath}");

            var inputFileStream = File.Open(inputFilePath, FileMode.Open);
            var outputFileStream = File.Create(outputFilePath);
            var gzipStream = new GZipStream(outputFileStream, CompressionLevel.Optimal);
            inputFileStream.CopyTo(gzipStream);
        }

        private static string GetChecksum(string audioFilePath)
        {
            using (var fileStream = File.Open(audioFilePath, FileMode.Open))
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                var md5Bytes = md5.ComputeHash(fileStream);
                return BitConverter.ToString(md5Bytes);
            }

        }

        static List<Metadata> GetMetadata(string metadataFilePath)
        {
            var metadataFileStream = File.Open(metadataFilePath, FileMode.Open);
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mmssZ")
            };
            var serializer = new DataContractJsonSerializer(typeof(List<Metadata>));
            return (List<Metadata>)serializer.ReadObject(metadataFileStream);
        }

        static void SaveSingleMetadata(Metadata metadata, string metadataFilePath)
        {
            var metadataFileStream = File.Open(metadataFilePath, FileMode.Create);
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mmssZ")
            };
            var serializer = new DataContractJsonSerializer(typeof(Metadata));
            serializer.WriteObject(metadataFileStream, metadata);
        }
    }
}
