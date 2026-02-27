using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Vapour.Models;

namespace Vapour.Test.ISteamWebAPIUtil
{
    public class GetServerInfoTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public GetServerInfoTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetServerInfo()
        {
            var response = await _client.GetAsync("/ISteamWebAPIUtil/GetServerInfo/v0001");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ServerInfo>(responseString, new JsonSerializerOptions());
            Assert.True(responseObject?.servertime > 0);
            Assert.Equal(responseObject?.servertimestring.Length, 24);
        }
    }
}
