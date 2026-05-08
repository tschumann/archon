using Microsoft.AspNetCore.Mvc.Testing;

namespace Vapour.Test
{
    public class BaseControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        protected readonly WebApplicationFactory<Program> _factory;

        // TODO: it's UTF-8 in the real API
        public static string ExpectedSuccessfulContentType = "application/json; charset=utf-8";

        public BaseControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
    }
}
