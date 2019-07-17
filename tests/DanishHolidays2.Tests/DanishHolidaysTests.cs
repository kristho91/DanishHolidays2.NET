using System;
using Xunit;
using DanishHolidays2;

namespace DanishHolidays2.Tests
{
    public class DanishHolidaysTests
    {
        [Fact(Skip = "Not yet implemented")]
        public void IsHoliday_Dummy_Test()
        {
        }

        [Fact]
        public void GetHolidays_ByYear2019_Test()
        {
            var result = DanishHoliday.GetHolidays(2019);

            Assert.Equal(16, result.Count);
        }
    }
}
