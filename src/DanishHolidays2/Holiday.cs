using System;

namespace DanishHolidays2
{
    public class Holiday
    {
        public Holiday(DateTime date, bool isDayOff, string name)
        {
            Date = date;
            IsDayOff = isDayOff;
            Name = name;
        }

        public DateTime Date { get; }
        public bool IsDayOff { get; }
        public string Name { get; }
    }
}
