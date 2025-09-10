using Vapour.Models.Internal;

namespace Vapour.Test.Models.Internal
{
    public class ServerListFilterTest
    {
        [Fact]
        public void TestToString()
        {
            var model = new ServerListFilter();
            Assert.Equal(string.Empty, model.ToString());

            model = new ServerListFilter()
            {
                appid = 70
            };
            Assert.Equal("appid=70", model.ToString());
        }
    }
}
