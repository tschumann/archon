using Archon.Valve.A2S;

namespace Archon.Test
{
    [TestClass]
    public sealed class QueryTest
    {
        [TestMethod]
        public void TestIsA2SQuery()
        {
            Assert.IsFalse(Archon.Valve.A2S.Query.IsA2SQuery(null));
        }
    }
}
