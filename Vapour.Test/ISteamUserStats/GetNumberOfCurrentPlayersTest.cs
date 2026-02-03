using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Vapour.ISteamUserStats;

namespace Vapour.Test.ISteamUserStats
{
    public class GetNumberOfCurrentPlayersTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public GetNumberOfCurrentPlayersTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayers()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetNumberOfCurrentPlayers/v1?appid=70");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"player_count\":1,\"result\":1}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoSuchAppid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetNumberOfCurrentPlayers/v1?appid=0");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"player_count\":1,\"result\":1}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersEmptyAppid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetNumberOfCurrentPlayers/v1?appid=");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"player_count\":1,\"result\":1}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersInvalidAppid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetNumberOfCurrentPlayers/v1?appid=a");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"response\":{\"player_count\":1,\"result\":1}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoAppid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetNumberOfCurrentPlayers/v1");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetNumberOfCurrentPlayers.MissingAppidErrorMessage, responseString);
        }
    }
}
