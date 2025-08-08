using Archon.MasterServers;

namespace Archon.Test.MasterServers
{
    [TestClass]
    public sealed class SourceMasterServerTest
    {
        [TestMethod]
        public void TestIsServerQuery()
        {
            Assert.IsFalse(SourceMasterServer.IsServerQuery([]));
            Assert.IsFalse(SourceMasterServer.IsServerQuery([0xFF]));
            Assert.IsFalse(SourceMasterServer.IsServerQuery([0x31, 0xFF]));
            Assert.IsTrue(SourceMasterServer.IsServerQuery([0x31, 0xFF, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x3A, 0x30, 0x00, 0x00]));
            Assert.IsTrue(SourceMasterServer.IsServerQuery([0x31, 0xFF, 0x31, 0x2E, 0x31, 0x2E, 0x31, 0x2E, 0x31, 0x3A, 0x30, 0x68, 0x87, 0x00]));
        }

        [TestMethod]
        public void TestIsValidRegionCode()
        {
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x00));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x01));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x02));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x03));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x04));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x05));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x06));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0x07));
            Assert.IsTrue(SourceMasterServer.IsValidRegionCode(0xFF));
            Assert.IsFalse(SourceMasterServer.IsValidRegionCode(0x08));
            Assert.IsFalse(SourceMasterServer.IsValidRegionCode(0x10));
        }
    }
}
