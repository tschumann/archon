using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Vapour.Test.IGameServersService
{
    public class GetServerListTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public GetServerListTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetServerListUnathenticated()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/");
            Assert.Equal(StatusCodes.Status403Forbidden, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(AuthMiddleware.UnathenticatedErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetServerListAuthenticated()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/?key=1");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"servers\":[{\"address\":\"127.0.0.1:27015\",\"gameport\":27015,\"steamid\":\"123\",\"name\":\"Server\",\"appid\":70,\"gamedir\":\"valve\",\"version\":\"1.0.0.0\",\"product\":\"Half-Life\",\"region\":255,\"players\":16,\"max_players\":32,\"bots\":0,\"map\":\"map\",\"secure\":true,\"dedicated\":true,\"os\":\"l\",\"gametype\":\"deathmatch\"},{\"address\":\"127.0.0.1:27015\",\"gameport\":27015,\"steamid\":\"123\",\"name\":\"Server\",\"appid\":50,\"gamedir\":\"gearbox\",\"version\":\"1.0.0.0\",\"product\":\"Half-Life: Opposing Force\",\"region\":255,\"players\":16,\"max_players\":32,\"bots\":0,\"map\":\"of\",\"secure\":true,\"dedicated\":true,\"os\":\"l\",\"gametype\":\"deathmatch\"}]}}", responseString);
        }

        [Fact]
        public async void TestGetServerListAuthenticatedFilterAppid()
        {
            var response = await _client.GetAsync("/IGameServersService/GetServerList/v1/?key=1&filter=appid\\70");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"servers\":[{\"address\":\"127.0.0.1:27015\",\"gameport\":27015,\"steamid\":\"123\",\"name\":\"Server\",\"appid\":70,\"gamedir\":\"valve\",\"version\":\"1.0.0.0\",\"product\":\"Half-Life\",\"region\":255,\"players\":16,\"max_players\":32,\"bots\":0,\"map\":\"map\",\"secure\":true,\"dedicated\":true,\"os\":\"l\",\"gametype\":\"deathmatch\"}]}}", responseString);

            response = await _client.GetAsync("/IGameServersService/GetServerList/v1/?key=1&filter=appid\\50");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"servers\":[{\"address\":\"127.0.0.1:27015\",\"gameport\":27015,\"steamid\":\"123\",\"name\":\"Server\",\"appid\":50,\"gamedir\":\"gearbox\",\"version\":\"1.0.0.0\",\"product\":\"Half-Life: Opposing Force\",\"region\":255,\"players\":16,\"max_players\":32,\"bots\":0,\"map\":\"of\",\"secure\":true,\"dedicated\":true,\"os\":\"l\",\"gametype\":\"deathmatch\"}]}}", responseString);

            response = await _client.GetAsync("/IGameServersService/GetServerList/v1/?key=1&filter=appid\\0");
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            response.EnsureSuccessStatusCode();
            responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"servers\":[]}}", responseString);
        }
    }
}