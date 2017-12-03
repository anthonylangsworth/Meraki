using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MerakiDashboard.Test
{
    public class TestLicenceExpirationDateFormat
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(string licenceExpirationDate, DateTime expectedDateTime)
        {
            Assert.Equal(expectedDateTime, LicenceExpirationDateFormat.ToDateTime(licenceExpirationDate));
            Assert.Equal(licenceExpirationDate, 
                LicenceExpirationDateFormat.FromDateTime(LicenceExpirationDateFormat.ToDateTime(licenceExpirationDate)));
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { "Aug 28, 2021 UTC", new DateTime(2021, 08, 28, 0, 0, 0, DateTimeKind.Utc) };
        }
    }
}
