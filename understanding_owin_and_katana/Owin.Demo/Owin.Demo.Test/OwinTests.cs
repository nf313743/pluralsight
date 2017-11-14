using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin.Demo.Hosting;

namespace Owin.Demo.Test
{
    [TestClass]
    public class OwinTests
    {
        [TestMethod]
        public async Task Owin_returns_200_on_request_to_root()
        {
            var status = await CallServer(async x =>
            {
                var response = await x.GetAsync("/");
                return response.StatusCode;
            });

            Assert.AreEqual(HttpStatusCode.OK, status);
        }

        [TestMethod]
        public async Task Owin_returns_hello_on_request_to_root()
        {
            var body = await CallServer(async x =>
            {
                var response = await x.GetAsync("/");
                return await response.Content.ReadAsStringAsync();
            });

            Assert.AreEqual("Hello World", body);
        }

        [TestMethod]
        public async Task Owin_returns_correct_contenttype_on_request_to_jpg()
        {
            var contenttype = await CallServer(async x =>
            {
                var response = await x.GetAsync("/sven.jpg");
                return response.Content.Headers.ContentType.MediaType;
            });

            Assert.AreEqual("image/jpeg", contenttype);
        }

        private async Task<T> CallServer<T>(Func<HttpClient, Task<T>> callback)
        {
            using (var server = TestServer.Create<Startup>())
            {
                return await callback(server.HttpClient);
            }
        }
    }
}