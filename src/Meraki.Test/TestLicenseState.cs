﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MerakiDashboard.Test
{
    public class TestLicenseState
    {
        [Theory]
        [MemberData(nameof(ExpirationDateRawTestData))]
        public void ExpirationDateRaw(string raw, DateTime expectedExpirationDate)
        {
            LicenceState licenseState = new LicenceState { ExpirationDateRaw = raw };
            Assert.Equal(expectedExpirationDate, licenseState.ExpirationDate.ToUniversalTime());
            Assert.Equal(raw, licenseState.ExpirationDateRaw);
        }

        public static IEnumerable<object[]> ExpirationDateRawTestData()
        {
            return TestLicenceExpirationDateFormat.ConvertTestData();
        }
    }
}
