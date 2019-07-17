using System;
using System.Collections.Generic;
using System.Linq;

namespace DanishHolidays2
{
    public static class DanishHoliday
    {
        /// <summary>
        /// Returns Easter Sunday based on year, month and day
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        private static void EasterSunday(int year, ref int month, ref int day)
        {
            int g = year % 19;
            int c = year / 100;
            int h = h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25)
                                                + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) *
                        (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) +
                          i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
        }

        /// <summary>
        /// Get easter sunday based on input year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static DateTime EasterSunday(int year)
        {
            var month = 0;
            var day = 0;
            EasterSunday(year, ref month, ref day);

            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Returns a boolean based on if the date matches a holiday.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime date)
        {
            var holiday = GetHolidays(date.Year)
                .FirstOrDefault(h => h.Date.ToShortDateString() == date.ToShortDateString());

            return holiday != null;
        }

        /// <summary>
        /// Overload of IsHoliday() 
        /// Returns a boolean and an out Holiday type if the date matches a holiday
        /// </summary>
        /// <param name="date"></param>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime date, out Holiday holiday)
        {
            holiday = GetHolidays(date.Year)
                .FirstOrDefault(h => h.Date.ToShortDateString() == date.ToShortDateString());

            return holiday != null;
        }

        /// <summary>
        /// Returns a list of holidays for the current year
        /// </summary>
        /// <returns></returns>
        public static List<Holiday> GetHolidays() => GetHolidays(DateTime.Now.Year);

        /// <summary>
        /// Overload for GetHolidays()
        /// Returns a list of holidays for the inputtet year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<Holiday> GetHolidays(int year)
        {
            const int ONE_DAY = 86400;

            var leapYear = DateTime.IsLeapYear(year);
            var fastelavn = leapYear ? 49 : 48;
            var easterSunday = EasterSunday(year);

            var holidays = new List<Holiday>
            {
                new Holiday(
                    date: new DateTime(year, 1, 1),
                    name: "Nytårsdag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.SubstractSeconds(3 * ONE_DAY),
                    name: "Skærtorsdag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.SubstractSeconds(2 * ONE_DAY),
                    name: "Langfredag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday,
                    name: "Påskedag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.AddSeconds(1 * ONE_DAY),
                    name: "2. Påskedag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.AddSeconds(26 * ONE_DAY),
                    name: "Store bededag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.AddSeconds(39 * ONE_DAY),
                    name: "Kr. himmelfartsdag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.AddSeconds(49 * ONE_DAY),
                    name: "Pinsedag",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.AddSeconds(50 * ONE_DAY),
                    name: "2. Pinsedag",
                    isDayOff: true),
                new Holiday(
                    date: new DateTime(year, 6, 5),
                    name: "Grundlovsdag",
                    isDayOff: true),
                new Holiday(
                    date: new DateTime(year, 12, 24),
                    name: "Juleaften",
                    isDayOff: true),
                new Holiday(
                    date: new DateTime(year, 12, 25),
                    name: "1. Juledag",
                    isDayOff: true),
                new Holiday(
                    date: new DateTime(year, 12, 26),
                    name: "2. Juledag",
                    isDayOff: true),
                new Holiday(
                    date: new DateTime(year, 12, 31),
                    name: "Nytårsaften",
                    isDayOff: true),
                new Holiday(
                    date: easterSunday.SubstractSeconds(fastelavn * ONE_DAY),
                    name: "Fastelavn",
                    isDayOff: false),
                new Holiday(
                    date: new DateTime(year, 1, 6),
                    name: "Hellig 3 Konger",
                    isDayOff: false)
            };

            return holidays;
        }
    }

    internal static class Extensions
    {
        /// <summary>
        /// Extension for substracting seconds, instead of using (date.AddSeconds(-{number})
        /// </summary>
        /// <param name="date"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        internal static DateTime SubstractSeconds(this DateTime date, int seconds) => date.AddSeconds(-seconds);
    }
}