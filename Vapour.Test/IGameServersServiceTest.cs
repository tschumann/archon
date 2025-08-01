using Microsoft.AspNetCore.Mvc.Testing;

namespace Vapour.Test
{
    public class IGameServersServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public IGameServersServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetServerList()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"name\":\"Server\",\"address\":\"127.0.0.1:27015\"}", responseString);
        }
    }
}