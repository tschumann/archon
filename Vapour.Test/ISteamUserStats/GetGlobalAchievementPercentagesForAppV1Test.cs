using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Vapour.ISteamUserStats;

namespace Vapour.Test.ISteamUserStats
{
    public class GetGlobalAchivementPercentagesForAppV1Test : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Program> _factory;

        public GetGlobalAchivementPercentagesForAppV1Test(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void TestGetGetGlobalAchievementPercentagesForAppNoAchievements()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=70");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{}", responseString);
        }

        [Fact]
        public async void TestGetGetGlobalAchievementPercentagesForApp()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=220");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"achievementpercentages\":{\"achievements\":{\"achievement\":[{\"name\":\"HL2_ESCAPE_APARTMENTRAID\",\"percent\":\"70.4\"}]}}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoSuchGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=0");
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersEmptyGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.BadRequestErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersInvalidGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=a");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.BadRequestErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.MissingGameidErrorMessage, responseString);
        }
    }
}
