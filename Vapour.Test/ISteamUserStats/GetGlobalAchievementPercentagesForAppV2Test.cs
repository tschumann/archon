using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Vapour.ISteamUserStats;

namespace Vapour.Test.ISteamUserStats
{
    public class GetGlobalAchivementPercentagesForAppV2Test : BaseControllerTest
    {
        public GetGlobalAchivementPercentagesForAppV2Test(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void TestGetGetGlobalAchievementPercentagesForAppNoAchievements()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=70");
            response.EnsureSuccessStatusCode();
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{}", responseString);
        }

        [Fact]
        public async void TestGetGetGlobalAchievementPercentagesForApp()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=220");
            response.EnsureSuccessStatusCode();
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"achievementpercentages\":{\"achievements\":[{\"name\":\"HL2_ESCAPE_APARTMENTRAID\",\"percent\":\"70.4\"}]}}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoSuchGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=0");
            response.EnsureSuccessStatusCode();
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{}", responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersEmptyGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.BadRequestErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersInvalidGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid=a");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.BadRequestErrorMessage, responseString);
        }

        [Fact]
        public async void TestGetNumberOfCurrentPlayersNoGameid()
        {
            var response = await _client.GetAsync("/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(GetGlobalAchievementPercentagesForAppV1.MissingGameidErrorMessage, responseString);
        }
    }
}
