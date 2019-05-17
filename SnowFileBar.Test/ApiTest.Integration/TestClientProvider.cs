using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SnowFileBar.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.Test.ApiTestIntegration
{
    public class TestClientProvider
    {
        public HttpClient Client { get; set; }
        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }
    }
}
