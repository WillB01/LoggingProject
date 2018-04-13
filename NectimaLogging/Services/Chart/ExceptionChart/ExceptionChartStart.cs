﻿using NectimaLogging.Controllers;
using NectimaLogging.Interface;
using NectimaLogging.Models;
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
        private ILogEntryRepository _logEntryRepository;
        private IWeek _week;
        private int _prevWeek;
        private bool _isPrev;
        private bool _isNext;
        public int MyCounter { get; set; }

        private double[] _procentOfExceptions = { 0, 0, 0, 0, 0, 0, 0 };
        private double[] _sumPerDay = { 0, 0, 0, 0, 0, 0, 0 };

        public string[] GetDays => _chartService.GetDays();


        public ExceptionChartStart(IChartService chartService, IWeek week, ILogEntryRepository logEntryRepository, int prevWeek, bool isPrev, bool isNext)
        {
            _logEntryRepository = logEntryRepository;
            _chartService = chartService;
            _week = week;
            _prevWeek = prevWeek;
            _isPrev = isPrev;
            _isNext = isNext;

        }

        public void Run()
        {
            WeekEntry();
            CheckIfPrevOrNext();
            DateTimeToString();
            Create();
            ExSum();
        }

        public void WeekEntry()
        {

            _week.SevenDaysFromToday = DateTime.Now.AddDays(-7);
            _week.SixDaysFromToday = DateTime.Now.AddDays(-6);
            _week.FiveDaysFromToday = DateTime.Now.AddDays(-5);
            _week.FourDaysFromToday = DateTime.Now.AddDays(-4);
            _week.ThreeDaysFromToday = DateTime.Now.AddDays(-3);
            _week.TwoDaysFromToday = DateTime.Now.AddDays(-2);
            _week.Yesterday = DateTime.Now.AddDays(-1);
            _week.Today = DateTime.Now;
        }

        public void CheckIfPrevOrNext()
        {
            if (_isPrev)
            {
                _prevWeek -= 7;
                MyCounter = _prevWeek;
            }
            if (_isNext)
            {
                _prevWeek += 7;
                MyCounter = _prevWeek;
            }

            _week.SevenDaysFromToday = _week.SevenDaysFromToday.AddDays(_prevWeek);
            _week.SixDaysFromToday = _week.SixDaysFromToday.AddDays(_prevWeek);
            _week.FiveDaysFromToday = _week.FiveDaysFromToday.AddDays(_prevWeek);
            _week.FourDaysFromToday = _week.FourDaysFromToday.AddDays(_prevWeek);
            _week.ThreeDaysFromToday = _week.ThreeDaysFromToday.AddDays(_prevWeek);
            _week.TwoDaysFromToday = _week.TwoDaysFromToday.AddDays(_prevWeek);
            _week.Yesterday = _week.Yesterday.AddDays(_prevWeek);
            _week.Today = _week.Today.AddDays(_prevWeek);

        }

        public void DateTimeToString()
        {
            _week.SevenDaysFromTodayString = _week.SevenDaysFromToday.RemoveTime();
            _week.SixDaysFromTodayString = _week.SixDaysFromToday.RemoveTime();
            _week.FiveDaysFromTodayString = _week.FiveDaysFromToday.RemoveTime();
            _week.FourDaysFromTodayString = _week.FourDaysFromToday.RemoveTime();
            _week.ThreeDaysFromTodayString = _week.ThreeDaysFromToday.RemoveTime();
            _week.TwoDaysFromTodayString = _week.TwoDaysFromToday.RemoveTime();
            _week.YesterdayString = _week.Yesterday.RemoveTime();
            _week.TodayString = _week.Today.RemoveTime();
        }

        public List<DateAndExcetionsRepository> Create()
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




        public List<ThreadNameAndThreadCount> ThreadCounter()
        {
            return _chartService.ThreadCounter();
        }

        public string[] ThreadName()
        {
            List<string> ta = new List<string>();
            foreach (var item in _chartService.ThreadCounter())
            {
                ta.Add($"{item.Thread}");
            }

            return ta.ToArray();
        }
        public int[] ThreadCount()
        {
            List<int> ta = new List<int>();
            foreach (var item in _chartService.ThreadCounter())
            {
                ta.Add(item.Amount);
            }

            return ta.ToArray();
        }

        public string[] LoggerName()
        {
            List<string> ta = new List<string>();
            foreach (var item in _chartService.LoggerKindCounters())
            {
                ta.Add(item.Logger.Logger);
            }

            return ta.ToArray();
        }
        public int[] LoggerCount()
        {
            List<int> ta = new List<int>();
            foreach (var item in _chartService.LoggerKindCounters())
            {
                ta.Add(item.Amount);
            }

            return ta.ToArray();
        }

        public double[] GetExProcentDay()
        {
            double[] convertToString = new double[7];

            for (int i = 0; i < _sumPerDay.Length; i++)
            {
                convertToString[i] = Math.Round(_sumPerDay[i], 2);
            }

            return convertToString;
        }

        


        //TESTIGN TESTING
        public List<AverageExceptions> ExSum()
        {
            var time = new AverageExceptions(_logEntryRepository);
            var firstMonthLog = _logEntryRepository.GetAllLogs.First(x => x.Id == 1);
            int firstMonthDate = int.Parse(firstMonthLog.Date.KeepOnlyMonth());
            string firstMonthDateString = firstMonthDate.ToString();


            if (firstMonthDate < 10)
                firstMonthDateString = $"0{firstMonthDateString}";

            foreach (var item in _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != ""))
            {

                var b = new DateTime(DateTime.Now.Year, int.Parse(item.Date.KeepOnlyMonth()), int.Parse(item.Date.KeepOnlyDay()));
                var dayof = b.DayOfWeek.ToString();

                switch (dayof)
                {
                    case "Monday":
                        _sumPerDay[0] += 1;
                        break;
                    case "Tuesday":
                        _sumPerDay[1] += 1;
                        break;
                    case "Wednesday":
                        _sumPerDay[2] += 1;
                        break;
                    case "Thursday":
                        _sumPerDay[3] += 1;
                        break;
                    case "Friday":
                        _sumPerDay[4] += 1;
                        break;
                    case "Saturday":
                        _sumPerDay[5] += 1;
                        break;
                    case "Sunday":
                        _sumPerDay[6] += 1;
                        break;
                    default:
                        break;
                }
             
            };

            for (int i = 0; i < _sumPerDay.Length; i++)
            {
                _sumPerDay[i] = (_sumPerDay[i] / _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != "").Count()) * 100;
            }

            List<AverageExceptions> avList = new List<AverageExceptions>()
            {
                new AverageExceptions(_logEntryRepository)
                {
                    Exceptions =  _logEntryRepository.GetAllLogs.Where(x =>
                    x.Exception != ""
                    && x.Date.KeepOnlyMonth() != null).Count(),

                    DaysInMonth =   DateTime.DaysInMonth(DateTime.Now.Year, firstMonthDate),

                },      
            };


            return avList;


        }


    }
}



