using NectimaLogging.Controllers;
using NectimaLogging.Interface;
using NectimaLogging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Services.Chart
{
    public class ExceptionChartStart
    {
        private IChartService _chartService;
        private IWeek _week;
        private int _prevWeek;

        public ExceptionChartStart(IChartService chartService, IWeek week, int prevWeek)
        {
            _chartService = chartService;
            _week = week;
           _prevWeek = prevWeek;
        }

        public void WeekEntry()
        {

            _week.SevenDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 3 - _prevWeek);
            _week.SixDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 2 - _prevWeek);
            _week.FiveDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1 - _prevWeek);
            _week.FourDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            _week.ThreeDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1 - _prevWeek);
            _week.TwoDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2 - _prevWeek);
            _week.Yesterday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3 - _prevWeek);
            _week.Today = DateTime.Now.Date.AddDays(- _prevWeek);




            _week.SevenDaysFromTodayString = _week.SevenDaysFromToday.RemoveTime();
            _week.SixDaysFromTodayString = _week.SixDaysFromToday.RemoveTime();
            _week.FiveDaysFromTodayString = _week.FiveDaysFromToday.RemoveTime();
            _week.FourDaysFromTodayString = _week.FourDaysFromToday.RemoveTime();
            _week.ThreeDaysFromTodayString = _week.ThreeDaysFromToday.RemoveTime();
            _week.TwoDaysFromTodayString = _week.TwoDaysFromToday.RemoveTime();
            _week.YesterdayString = _week.Yesterday.RemoveTime();
            _week.TodayString = _week.Today.RemoveTime();

        }


        public List<DateAndExcetionsRepository> Start(int prevWeek)
        {

            WeekEntry();


            List<DateAndExcetionsRepository> dateAndExcetionsRepository = new List<DateAndExcetionsRepository>()
            {
                new DateAndExcetionsRepository()
                {   PrevWeek = _week.PrevWeek = _prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SevenDaysFromTodayString),
                    Date = _week.SevenDaysFromTodayString


                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SixDaysFromTodayString),
                    Date = _week.SixDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FiveDaysFromTodayString),
                    Date = _week.FiveDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FourDaysFromTodayString),
                    Date = _week.FourDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.ThreeDaysFromTodayString),
                    Date = _week.ThreeDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TwoDaysFromTodayString),
                    Date = _week.TwoDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.YesterdayString),
                    Date = _week.YesterdayString
                },
                new DateAndExcetionsRepository()
                {PrevWeek = prevWeek,
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TodayString),
                    Date = _week.TodayString
                },
            };

            return dateAndExcetionsRepository;
        }
    }
}
