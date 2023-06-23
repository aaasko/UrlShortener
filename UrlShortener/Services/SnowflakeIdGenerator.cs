using Microsoft.Extensions.Options;
using UrlShortener.Options;

namespace UrlShortener.Services
{
    /// <summary>
    /// 1 bit - 0, 41 bits - timestamp, 5 bits - datacenter ID, 5 bits - machine ID, 12 bits - sequence number
    /// </summary>
    public class SnowflakeIdGenerator : IIdGenerator
    {
        public static readonly DateTime EpochStart = new(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private const long MaxSequenceNumber = (1 << 12) - 1;
        private static readonly long Epoch = new DateTimeOffset(EpochStart).ToUnixTimeMilliseconds();

        private readonly object _lock = new();
        private readonly MachineOptions _machineOptions;

        private readonly IDateTime _dateTime;
        private long _lastTimestamp = -1;
        private long _sequenceNumber;

        public SnowflakeIdGenerator(IDateTime dateTime, IOptions<MachineOptions> machineOptions) {
            _dateTime = dateTime;
            _machineOptions = machineOptions.Value;
        }

        public long GenerateId()
        {
            var dateTime = _dateTime.UtcNow();
            var timestamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds() - Epoch;

            if (timestamp < 0)
            {
                throw new NotSupportedException("Timestamps before 2023-01-01 UTC are not supported");
            }

            long sequenceNumber;

            lock (_lock)
            {
                if (timestamp != _lastTimestamp)
                {
                    _lastTimestamp = timestamp;
                    _sequenceNumber = 0;
                }
                else
                {
                    _sequenceNumber++;

                    if (_sequenceNumber > MaxSequenceNumber)
                    {
                        throw new NotSupportedException($"Cannot generate more than {MaxSequenceNumber} IDs in one millisecond");
                    }
                }

                sequenceNumber = _sequenceNumber;
            }

            return (timestamp << 22)
                | (_machineOptions.DataCenterId << 17)
                | (_machineOptions.MachineId << 12)
                | sequenceNumber;
        }
    }
}
