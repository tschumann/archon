using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Vapour.Models.Responses;

namespace Vapour.Test.IClientStats
{
    public class ReportEventTest : BaseControllerTest
    {
        public ReportEventTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void TestReportEventInvalid()
        {
            var response = await _client.PostAsync("/IClientStats_1046930/ReportEvent/v1/", new StringContent(""));
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
            Assert.Equal(ExpectedSuccessfulContentType, response.Content.Headers?.ContentType?.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ReportEventResponse>(responseString, jsonSerialiserOptions);
            Assert.False(responseObject?.success);
            Assert.Equal("", responseObject?.message);
        }
    }
}
