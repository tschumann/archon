using Microsoft.AspNetCore.Mvc.Testing;

namespace Vapour.Test.ISteamWebAPIUtil
{
    public class GetSupportedAPIListTest : BaseControllerTest
    {
        public GetSupportedAPIListTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void TestGetSupportedAPIList()
        {
            var response = await _client.GetAsync("/ISteamWebAPIUtil/GetSupportedAPIList/v0001/");
            response.EnsureSuccessStatusCode();
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"apilist\":{\"interfaces\":[{\"name\":\"IGameServersService\",\"methods\":[{\"name\":\"GetServerList\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]},{\"name\":\"ISteamUserStats\",\"methods\":[{\"name\":\"GetGlobalAchievementPercentagesForApp\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]},{\"name\":\"GetGlobalAchievementPercentagesForApp\",\"version\":2,\"httpmethod\":\"GET\",\"parameters\":[]},{\"name\":\"GetNumberOfCurrentPlayers\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]},{\"name\":\"ISteamWebAPIUtil\",\"methods\":[{\"name\":\"GetServerInfo\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]},{\"name\":\"GetSupportedAPIList\",\"version\":1,\"httpmethod\":\"GET\",\"parameters\":[]}]}]}}", responseString);
        }
    }
}
