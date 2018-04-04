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
        private bool _isNext;
        public int MyCounter { get; set; }
        public DateTime Seven { get; set; }
        public DateTime Six { get; set; }
        public DateTime Five { get; set; }
        public DateTime Four { get; set; }
        public DateTime Three { get; set; }
        public DateTime Two { get; set; }
        public DateTime Yesterday { get; set; }
        public DateTime Today { get; set; }


        public ExceptionChartStart(IChartService chartService, IWeek week, int prevWeek, bool isPrev, bool isNext)
        {
            _chartService = chartService;
            _week = week;
            _prevWeek = prevWeek;
            _isPrev = isPrev;
            _isNext = isNext;

            WeekEntry();
            Start();



        }

        public void WeekEntry()
        {
          
            Seven = _week.SevenDaysFromToday = DateTime.Now.AddDays(-7);
            Six = _week.SixDaysFromToday = DateTime.Now.AddDays(-6);
            Five = _week.SixDaysFromToday = DateTime.Now.AddDays(-5);
            Four = _week.FourDaysFromToday = DateTime.Now.AddDays(-4);
            Three = _week.ThreeDaysFromToday = DateTime.Now.AddDays(-3);
            Two = _week.TwoDaysFromToday = DateTime.Now.AddDays(-2);
            Yesterday = _week.Yesterday = DateTime.Now.AddDays(-1);
            Today = _week.Today = DateTime.Now;

            if (_isPrev)
            {
                Seven = Seven.AddDays(_prevWeek);
                Six = Six.AddDays(_prevWeek);
                Five = Five.AddDays(_prevWeek);
                Four = Four.AddDays(_prevWeek);
                Three = Three.AddDays(_prevWeek);
                Two = Two.AddDays(_prevWeek);
                Yesterday = Yesterday.AddDays(_prevWeek);
                Today = Today.AddDays(_prevWeek);

                _prevWeek -= 7;
                MyCounter = _prevWeek;
            }
            if (_isNext)
            {
                Seven = Seven.AddDays(_prevWeek);
                Six = Six.AddDays(_prevWeek);
                Five = Five.AddDays(_prevWeek);
                Four = Four.AddDays(_prevWeek);
                Three = Three.AddDays(_prevWeek);
                Two = Two.AddDays(_prevWeek);
                Yesterday = Yesterday.AddDays(_prevWeek);
                Today = Today.AddDays(_prevWeek);

                _prevWeek += 7;
                MyCounter = _prevWeek;
            }


            _week.SevenDaysFromTodayString = Seven.RemoveTime();
            _week.SixDaysFromTodayString = Six.RemoveTime();
            _week.FiveDaysFromTodayString = Five.RemoveTime();
            _week.FourDaysFromTodayString = Four.RemoveTime();
            _week.ThreeDaysFromTodayString = Three.RemoveTime();
            _week.TwoDaysFromTodayString = Two.RemoveTime();
            _week.YesterdayString = Yesterday.RemoveTime();
            _week.TodayString = Today.RemoveTime();

        }

        public List<DateAndExcetionsRepository> Start()
        {


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
