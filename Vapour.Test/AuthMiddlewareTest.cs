namespace Vapour.Test
{
    public class AuthMiddlewareTest
    {
        [Fact]
        public void TestAuthMetadataAttribute()
        {
            var attribute = new AuthMetadata("unused");
            Assert.Equal("unused", attribute.MetadataValue);
        }
    }
}
