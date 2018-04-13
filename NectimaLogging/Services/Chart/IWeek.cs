using System;
using System.Collections.Generic;

namespace NectimaLogging.Controllers
{
    public interface IWeek
    {
        DateTime SevenDaysFromToday { get; set; }
        DateTime SixDaysFromToday { get; set; }
        DateTime FiveDaysFromToday { get; set; }
        DateTime FourDaysFromToday { get; set; }
        DateTime ThreeDaysFromToday { get; set; }
        DateTime TwoDaysFromToday { get; set; }
        DateTime Yesterday { get; set; }
        DateTime Today { get; set; }

        string SevenDaysFromTodayString { get; set; }
        string SixDaysFromTodayString { get; set; }
        string FiveDaysFromTodayString { get; set; }
        string FourDaysFromTodayString { get; set; }
        string ThreeDaysFromTodayString { get; set; }
        string TwoDaysFromTodayString { get; set; }
        string YesterdayString { get; set; }
        string TodayString { get; set; }


        int PrevWeek { get; set; }
    }









}