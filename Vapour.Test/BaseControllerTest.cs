using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vapour.Test
{
    public class BaseControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        protected readonly WebApplicationFactory<Program> _factory;

        // make sure extra fields don't come through as it should match the real thing
        public JsonSerializerOptions jsonSerialiserOptions = new JsonSerializerOptions()
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };

        // TODO: it's UTF-8 in the real API
        public static string ExpectedSuccessfulContentType = "application/json; charset=utf-8";

        public BaseControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
    }
}
