using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vapour.Models.Responses;

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
            var responseObject = JsonSerializer.Deserialize<ServerInfoResponse>(responseString, new JsonSerializerOptions()
            {
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
            });
            Assert.True(responseObject?.servertime > 0);
            Assert.Equal(responseObject?.servertimestring.Length, 24);
        }
    }
}
