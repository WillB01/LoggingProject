using System;
using System.Collections.Generic;

namespace NectimaLogging.Controllers
{
    public class WeekEntry : IWeek
    {

        public DateTime SevenDaysFromToday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 3);*/
        public DateTime SixDaysFromToday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 2);*/
        public DateTime FiveDaysFromToday { get; set; }/* = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1);*/
        public DateTime FourDaysFromToday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);*/
        public DateTime ThreeDaysFromToday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);*/
        public DateTime TwoDaysFromToday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2);*/
        public DateTime Yesterday { get; set; } /*= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3);*/
        public DateTime Today { get; set; }/* = DateTime.Now.Date;*/

        public string SevenDaysFromTodayString { get; set; }
        public string SixDaysFromTodayString { get; set; }
        public string FiveDaysFromTodayString { get; set; }
        public string FourDaysFromTodayString { get; set; }
        public string ThreeDaysFromTodayString { get; set; }
        public string TwoDaysFromTodayString { get; set; }
        public string YesterdayString { get; set; }
        public string TodayString { get; set; }

        public List<string> Days { get; set; }

        public int PrevWeek { get; set; } = 0;




        //public WeekEntry()
        //{
           
        //    SevenDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 3 - PrevWeek);
        //    SixDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 2 - PrevWeek);
        //    FiveDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1 - PrevWeek);
        //    FourDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        //    ThreeDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1 - PrevWeek);
        //    TwoDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2 - PrevWeek);
        //    Yesterday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3 - PrevWeek);
        //    Today  = DateTime.Now.Date.AddDays(- PrevWeek);




        //    SevenDaysFromTodayString = SevenDaysFromToday.RemoveTime();
        //    SixDaysFromTodayString = SixDaysFromToday.RemoveTime();
        //    FiveDaysFromTodayString = FiveDaysFromToday.RemoveTime();
        //    FourDaysFromTodayString = FourDaysFromToday.RemoveTime();
        //    ThreeDaysFromTodayString = ThreeDaysFromToday.RemoveTime();
        //    TwoDaysFromTodayString = TwoDaysFromToday.RemoveTime();
        //    YesterdayString = Yesterday.RemoveTime();
        //    TodayString = Today.RemoveTime();

        //}

       

    

        public List<string> GetDays()
        {
            Days = new List<string>
            {

                SevenDaysFromTodayString,
                SixDaysFromTodayString,
                FiveDaysFromTodayString,
                FourDaysFromTodayString,
                ThreeDaysFromTodayString,
                TwoDaysFromTodayString,
                YesterdayString,
                TodayString

            };



            return Days;
        }

    }









}