using System;
using System.Collections.Generic;

namespace NectimaLogging.Controllers
{
    public class WeekEntry : IWeek
    {

        public DateTime SevenDaysFromToday { get; set; } 
        public DateTime SixDaysFromToday { get; set; } 
        public DateTime FiveDaysFromToday { get; set; }
        public DateTime FourDaysFromToday { get; set; } 
        public DateTime ThreeDaysFromToday { get; set; } 
        public DateTime TwoDaysFromToday { get; set; } 
        public DateTime Yesterday { get; set; } 
        public DateTime Today { get; set; }

        public string SevenDaysFromTodayString { get; set; }
        public string SixDaysFromTodayString { get; set; }
        public string FiveDaysFromTodayString { get; set; }
        public string FourDaysFromTodayString { get; set; }
        public string ThreeDaysFromTodayString { get; set; }
        public string TwoDaysFromTodayString { get; set; }
        public string YesterdayString { get; set; }
        public string TodayString { get; set; }

        

        public int PrevWeek { get; set; } = 0;

        

    }









}