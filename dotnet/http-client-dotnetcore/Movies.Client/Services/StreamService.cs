using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.Services
{
    public class StreamService : IIntegrationService
    {
        private static HttpClient HttpClient = new HttpClient(
            new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });

        public StreamService()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:57863");
            HttpClient.Timeout = new TimeSpan(0, 0, 30);
            HttpClient.DefaultRequestHeaders.Clear();
            //HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Run()
        {
            // await GetPosterWithStream();
            // await GetPosterWithStreamAndCompletionMode();

            //await TestGetPosterWithoutStream();
            //await TestGetPosterWithStream();
            //await TestGetPosterWithStreamAndCompletionMode();

            // await PostPosterWithStream();
            // await PostAndReadPosterWithStreams();

            await GetPosterWithGZipCompression();
        }

        private async Task GetPosterWithStream()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await HttpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var poster = stream.ReadAndDeserializerFromJson<Poster>();
            }
        }

        private async Task GetPosterWithStreamAndCompletionMode()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var poster = stream.ReadAndDeserializerFromJson<Poster>();
            }
        }

        private async Task GetPosterWithoutStream()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await HttpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var poster = JsonConvert.DeserializeObject<Poster>(content);
        }

        private async Task TestGetPosterWithoutStream()
        {
            // Warmup
            await GetPosterWithoutStream();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; ++i)
            {
                await GetPosterWithoutStream();
            }

            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds without stream: {stopWatch.ElapsedMilliseconds}, averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/requests");
        }

        private async Task TestGetPosterWithStream()
        {
            // Warmup
            await GetPosterWithStream();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; ++i)
            {
                await GetPosterWithStream();
            }

            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream: {stopWatch.ElapsedMilliseconds}, averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/requests");
        }

        private async Task TestGetPosterWithStreamAndCompletionMode()
        {
            // Warmup
            await GetPosterWithStreamAndCompletionMode();

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 200; ++i)
            {
                await GetPosterWithStreamAndCompletionMode();
            }

            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream and completionmode: {stopWatch.ElapsedMilliseconds}, averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/requests");
        }

        private async Task PostPosterWithStream()
        {
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(posterForCreation);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await HttpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var createdContent = await response.Content.ReadAsStringAsync();
                    var createdPoster = JsonConvert.DeserializeObject<Poster>(createdContent);
                }
            }
        }

        private async Task PostAndReadPosterWithStreams()
        {
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(posterForCreation);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();

                        var stream = await response.Content.ReadAsStreamAsync();
                        var createdPoster = stream.ReadAndDeserializerFromJson<Poster>();
                    }
                }
            }
        }

        private async Task GetPosterWithGZipCompression()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var response = await HttpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var poster = stream.ReadAndDeserializerFromJson<Poster>();
            }
        }
    }
}