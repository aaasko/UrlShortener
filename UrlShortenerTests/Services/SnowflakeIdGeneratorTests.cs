using UrlShortener.Options;
using UrlShortener.Services;
using UrlShortenerTests.TestUtils;
using FixedOptions = Microsoft.Extensions.Options.Options;

namespace UrlShortenerTests.Services
{
    [TestClass]
    public class SnowflakeIdGeneratorTests
    {
        [TestMethod]
        public void EpochStart_MustBe20230101()
        {
            Assert.AreEqual(new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc), SnowflakeIdGenerator.EpochStart);
        }

        [TestMethod]
        public void GenerateId_MustReturnZeroFor20230101()
        {
            var generator = ShowflakeIdGeneratorAt20230101();

            Assert.AreEqual(0, generator.GenerateId());
        }

        [TestMethod]
        public void GenerateIdCalledTwiceSameMillisecond_MustReturnConsecutiveNumbers()
        {
            var generator = ShowflakeIdGeneratorAt20230101();

            Assert.AreEqual(0, generator.GenerateId());
            Assert.AreEqual(1, generator.GenerateId());
        }

        private static SnowflakeIdGenerator ShowflakeIdGeneratorAt20230101()
        {
            var dateTime = FixedDateTime.Create(new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            var machineOptions = FixedOptions.Create(new MachineOptions());

            return new SnowflakeIdGenerator(dateTime, machineOptions);
        }
    }
}