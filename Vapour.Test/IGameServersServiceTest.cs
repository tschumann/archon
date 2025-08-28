using Microsoft.AspNetCore.Http;
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
        public async void TestGetServerListUnathenticated()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/");
            Assert.Equal(StatusCodes.Status403Forbidden, (int)response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(AuthMiddleware.UnathenticatedErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetServerListAuthenticated()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/?key=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"name\":\"Server\",\"address\":\"127.0.0.1:27015\"}", responseString);
        }
    }
}