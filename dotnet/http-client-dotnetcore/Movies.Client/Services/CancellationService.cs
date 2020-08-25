using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Movies.Client.Models;

namespace Movies.Client.Services
{
    public class CancellationService : IIntegrationService
    {
        private static HttpClient HttpClient = new HttpClient(
            new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();


        public CancellationService()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:57863");
            HttpClient.Timeout = new TimeSpan(0, 0, 30);
            HttpClient.DefaultRequestHeaders.Clear();
            //HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Run()
        {
            _cancellationTokenSource.CancelAfter(2000);
            await GetTrailerAndCancel(_cancellationTokenSource.Token);
        }

        private async Task GetTrailerAndCancel(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/trailers/{Guid.NewGuid()}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


            try
            {
                using (var response = await HttpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken))
                {
                    var steam = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();
                    var trailer = steam.ReadAndDeserializerFromJson<Trailer>();
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Operation cancelled: {ex.Message}");
            }

        }

        private async Task GetTrailerAndHandleTimeout()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/D8663E5E-7494-4F81-8739-6E0DE1BEA7EE/trailers/{Guid.NewGuid()}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


            try
            {
                using (var response = await HttpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead))
                {
                    var steam = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();
                    var trailer = steam.ReadAndDeserializerFromJson<Trailer>();
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Operation cancelled: {ex.Message}");
            }
        }
    }
