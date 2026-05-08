using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Vapour.Models;

namespace Vapour.Test.ISteamWebAPIUtil
{
    public class GetServerInfoTest : BaseControllerTest
    {
        public GetServerInfoTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void TestGetServerInfo()
        {
            var response = await _client.GetAsync("/ISteamWebAPIUtil/GetServerInfo/v0001/");
            response.EnsureSuccessStatusCode();
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ServerInfo>(responseString, new JsonSerializerOptions());
            Assert.True(responseObject?.servertime > 0);
            Assert.Equal(responseObject?.servertimestring.Length, 24);
        }
    }
}
