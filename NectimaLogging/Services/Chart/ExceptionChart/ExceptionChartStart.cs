using NectimaLogging.Controllers;
using NectimaLogging.Interface;
using NectimaLogging.Repository;
using NectimaLogging.Services.Chart.ExceptionChart;
using NectimaLogging.ViewModels;
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
        private bool _isPrev;
        public int MyCounter{ get; set; }







        public ExceptionChartStart(IChartService chartService, IWeek week, int prevWeek, bool isPrev)
        {
            _chartService = chartService;
            _week = week;
           _prevWeek = prevWeek;
           _isPrev = isPrev;
          
            
        }

        public void WeekEntry()
        {
            var b = new MyCounter()
            {
                Counter = _prevWeek
            };
            if (_isPrev)
            {
                _prevWeek++;

                MyCounter = _prevWeek;
            }
            
         
            var one = _week.SevenDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 5 - _prevWeek);
            var two = _week.SixDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 4 - _prevWeek);
            var three = _week.FiveDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 3 - _prevWeek);
            var four = _week.FourDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 2 - _prevWeek);
            var five = _week.ThreeDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1 - _prevWeek);
            var six = _week.TwoDaysFromToday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 0 - _prevWeek);
            var seven = _week.Yesterday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1 - _prevWeek);
            var eight = _week.Today = DateTime.Now.Date.AddDays(-_prevWeek);

        

            _week.SevenDaysFromTodayString = one.RemoveTime();
            _week.SixDaysFromTodayString = two.RemoveTime();
            _week.FiveDaysFromTodayString = three.RemoveTime();
            _week.FourDaysFromTodayString = four.RemoveTime();
            _week.ThreeDaysFromTodayString = five.RemoveTime();
            _week.TwoDaysFromTodayString = six.RemoveTime();
            _week.YesterdayString = seven.RemoveTime();
            _week.TodayString = eight.RemoveTime();

        }

      
        public List<DateAndExcetionsRepository> Start()
        {
         
            WeekEntry();
            
     
        
            List<DateAndExcetionsRepository> dateAndExcetionsRepository = new List<DateAndExcetionsRepository>()
            {
                new DateAndExcetionsRepository()
                {
                    
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SevenDaysFromTodayString),
                    Date =  _week.SevenDaysFromTodayString


                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SixDaysFromTodayString),
                    Date = _week.SixDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FiveDaysFromTodayString),
                    Date = _week.FiveDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FourDaysFromTodayString),
                    Date = _week.FourDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.ThreeDaysFromTodayString),
                    Date = _week.ThreeDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TwoDaysFromTodayString),
                    Date = _week.TwoDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.YesterdayString),
                    Date = _week.YesterdayString
                },
                new DateAndExcetionsRepository()
                {
                    AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TodayString),
                    Date = _week.TodayString
                },
            };

            return dateAndExcetionsRepository;
        }
    }
}
