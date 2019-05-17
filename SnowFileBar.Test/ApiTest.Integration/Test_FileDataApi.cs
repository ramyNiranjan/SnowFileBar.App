using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SnowFileBar.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SnowFileBar.Test.ApiTestIntegration
{
    
    public class Test_FileDataApi
    {
        private readonly HttpClient client;
        public Test_FileDataApi()
        {
            var server = new TestServer(new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>());
            client = server.CreateClient();
        }
        [Theory]
        [InlineData("GET")]
        public async Task Test_GetAllFileData(string value)
        {

            var req = new HttpRequestMessage(new HttpMethod(value), "/api/filedataapi");
            var res = await client.SendAsync(req);
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
           
        }
        [Theory]
        [InlineData("GET", 1)]
        public async Task Test_GetFileDataAsync(string method, int id = 1)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/filedataapi{id}");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
