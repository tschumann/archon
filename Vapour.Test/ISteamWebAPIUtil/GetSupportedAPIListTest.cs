using Microsoft.AspNetCore.Mvc.Testing;

namespace Vapour.Test.ISteamWebAPIUtil
{
    public class GetSupportedAPIListTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public GetSupportedAPIListTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetSupportedAPIList()
        {
            var response = await _client.GetAsync("/ISteamWebAPIUtil/GetSupportedAPIList/v0001");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"apilist\":{\"interfaces\":[{\"name\":\"IGameServersService\",\"methods\":[{\"name\":\"GetServerList\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]},{\"name\":\"ISteamUserStats\",\"methods\":[{\"name\":\"GetNumberOfCurrentPlayers\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]},{\"name\":\"ISteamWebAPIUtil\",\"methods\":[{\"name\":\"GetServerInfo\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]},{\"name\":\"GetSupportedAPIList\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]}]}}", responseString);
        }
    }
}
