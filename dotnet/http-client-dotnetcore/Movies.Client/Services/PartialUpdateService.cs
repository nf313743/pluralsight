using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.Services
{
    public class PartialUpdateService : IIntegrationService
    {
        private static HttpClient HttpClient = new HttpClient();

        public PartialUpdateService()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:57863");
            HttpClient.Timeout = new TimeSpan(0, 0, 30);
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Run()
        {
            //await PatchResource();
            await PatchResourceShortcut();
        }

        private async Task PatchResource()
        {
            var patchDoc = new JsonPatchDocument<MovieForUpdate>();

            patchDoc.Replace(x => x.Title, "Updated Title");
            patchDoc.Remove(x => x.Description);

            var serializedChangeSet = JsonConvert.SerializeObject(patchDoc);

            var request = new HttpRequestMessage(
                HttpMethod.Patch,
                "api/movies/5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serializedChangeSet);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
        }

        private async Task PatchResourceShortcut()
        {
            var patchDoc = new JsonPatchDocument<MovieForUpdate>();

            patchDoc.Replace(x => x.Title, "Updated Title");
            patchDoc.Remove(x => x.Description);

            var response = await HttpClient.PatchAsync(
                "api/movies/5b1c2b4d-48c7-402a-80c3-cc796ad49c6b",
                new StringContent(
                    JsonConvert.SerializeObject(patchDoc),
                    Encoding.UTF8,
                    "application/json-patch+json"));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
        }
    }
}