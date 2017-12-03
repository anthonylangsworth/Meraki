using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MerakiDashboard.Test
{
    public class TestUnixTimestamp
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(double timestamp, DateTime expectedDateTime)
        {
            Assert.Equal(expectedDateTime, UnixTimestamp.ToDateTime(timestamp));
            Assert.Equal(timestamp, UnixTimestamp.FromDateTime(UnixTimestamp.ToDateTime(timestamp)), 3);
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { "1474918068.66194", DateTime.Parse("2016-09-26T19:27:48.6620000") };
            yield return new object[] { "1474918295.0268", DateTime.Parse("2016-05-05T19:56:57.9490000") };
            yield return new object[] { "1462478217.94907", DateTime.Parse("2016-05-05T19:56:57.9490000") };
            yield return new object[] { "0.0", UnixTimestamp.EpochStart };
            yield return new object[] { "-100.0", UnixTimestamp.EpochStart.AddSeconds(-100.0) };
        }
    }
}
