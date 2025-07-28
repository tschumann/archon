using Ludum.Valve.A2S;

namespace Ludum.Test
{
    [TestClass]
    public sealed class QueryTest
    {
        [TestMethod]
        public void TestIsA2SQuery()
        {
            Assert.IsFalse(Query.IsA2SQuery([]));
            Assert.IsFalse(Query.IsA2SQuery([0x00]));
            Assert.IsFalse(Query.IsA2SQuery([0x00, 0x00]));
            Assert.IsFalse(Query.IsA2SQuery([0xFF]));
            Assert.IsFalse(Query.IsA2SQuery([0xFF, 0xFF, 0xFF, 0xFF]));
            Assert.IsTrue(Query.IsA2SQuery([0xFF, 0xFF, 0xFF, 0xFF, 0xFF]));
        }

        [TestMethod]
        public void TestIsInfoRequest()
        {
            Assert.IsFalse(Query.IsInfoRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x00]));
            Assert.IsFalse(Query.IsInfoRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x55]));
            Assert.IsTrue(Query.IsInfoRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x54, ((byte)'S'), ((byte)'o'), ((byte)'u'), ((byte)'r'), ((byte)'c'), ((byte)'e'), ((byte)' '), ((byte)'E'), ((byte)'n'), ((byte)'g'), ((byte)'i'), ((byte)'n'), ((byte)'e'), ((byte)' '), ((byte)'Q'), ((byte)'u'), ((byte)'e'), ((byte)'r'), ((byte)'y'), 0x00]));
            Assert.IsTrue(Query.IsInfoRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x54, ((byte)'S'), ((byte)'o'), ((byte)'u'), ((byte)'r'), ((byte)'c'), ((byte)'e'), ((byte)' '), ((byte)'E'), ((byte)'n'), ((byte)'g'), ((byte)'i'), ((byte)'n'), ((byte)'e'), ((byte)' '), ((byte)'Q'), ((byte)'u'), ((byte)'e'), ((byte)'r'), ((byte)'y'), 0x00, 0x01, 0x02, 0x03, 0x05]));
            Assert.IsTrue(Query.IsInfoRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x54, ((byte)'S'), ((byte)'o'), ((byte)'u'), ((byte)'r'), ((byte)'c'), ((byte)'e'), ((byte)' '), ((byte)'E'), ((byte)'n'), ((byte)'g'), ((byte)'i'), ((byte)'n'), ((byte)'e'), ((byte)' '), ((byte)'Q'), ((byte)'u'), ((byte)'e'), ((byte)'r'), ((byte)'y'), 0x00, 0xF1, 0xF2, 0xF3, 0xF5]));
        }

        [TestMethod]
        public void TestIsPlayerInfoRequest()
        {
            Assert.IsFalse(Query.IsPlayerRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x00]));
            Assert.IsFalse(Query.IsPlayerRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x55]));
            Assert.IsTrue(Query.IsPlayerRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF]));
        }

        [TestMethod]
        public void TestIsRulesRequest()
        {
            Assert.IsFalse(Query.IsRulesRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x00]));
            Assert.IsFalse(Query.IsRulesRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x56]));
            Assert.IsTrue(Query.IsRulesRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x56, 0xFF, 0xFF, 0xFF, 0xFF]));
        }

        [TestMethod]
        public void TestIsPingRequest()
        {
            Assert.IsFalse(Query.IsPingRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x00]));
            Assert.IsTrue(Query.IsPingRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x69]));
        }

        [TestMethod]
        public void TestIsServerQueryGetChallengeRequest()
        {
            Assert.IsFalse(Query.IsServerQueryGetChallengeRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x00]));
            Assert.IsTrue(Query.IsServerQueryGetChallengeRequest([0xFF, 0xFF, 0xFF, 0xFF, 0x57]));
        }
    }
}
