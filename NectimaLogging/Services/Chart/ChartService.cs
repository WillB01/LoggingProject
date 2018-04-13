using NectimaLogging.Controllers;
using NectimaLogging.Helpers;
using NectimaLogging.Interface;
using NectimaLogging.Models;
using NectimaLogging.Services.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class ChartService : IChartService
    {
        private ILogEntryRepository _logEntryRepository;
        private IWeek _week;
        private IEnumerable<LogEntry> _test;

        private double[] _procentOfExceptions = { 0, 0, 0, 0, 0, 0, 0 };
        private double[] _sumPerDay = { 0, 0, 0, 0, 0, 0, 0 };

        private LogEntry _firstMonthLog;
        private int _firstMonthDate;
        private string _firstMonthDateString;
        private AverageExceptions _time;
        private DateTime _dayOfWeekDate;
        private string _dayOfWeek;

       

        public ChartService(ILogEntryRepository logEntryRepository, IWeek week)
        {
            _logEntryRepository = logEntryRepository;
            _week = week;

            _time = new AverageExceptions(_logEntryRepository);
            _firstMonthLog = _logEntryRepository.GetAllLogs.First(x => x.Id == 1);
            _firstMonthDate = int.Parse(_firstMonthLog.Date.KeepOnlyMonth());
            _firstMonthDateString = _firstMonthDate.ToString();
        }

        public int AmountOfExceptions()
        {
            _test = _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != "");

            return _test.Count(p => p.Exception != "");
        }

        public IEnumerable<LogEntry> ExcetionsPerDay()
        {

            _test = _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != "");
            return _test;
        }



        public int DebugLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Info").Count();
        }

        public int ErrorLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Error").Count();
        }
        public int FatalLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Fatal").Count();
        }
        public int InfoLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Info").Count();
        }
        public int OffLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Off").Count();
        }
        public int WarnLevelCounter()
        {
            return _logEntryRepository.GetAllLogs.Where(x => x.Level == "Warn").Count();
        }

        public List<ThreadNameAndThreadCount> ThreadCounter()
        {
            var results = _logEntryRepository.GetAllLogs
              .OrderByDescending(item => item.Thread)
            .GroupBy(item => item.Thread)
            .Select(x => new ThreadNameAndThreadCount() { Thread = x.Key, Amount = x.Count()})
            .ToList();
            
            
            return results;
        }

        public List<LoggerKindCounter> LoggerKindCounters()
        {
            var results = _logEntryRepository.GetAllLogs
            .OrderByDescending(item => item.Logger)
          .GroupBy(item => item.Thread)
          .Select(x => new LoggerKindCounter() { Logger = x.First()  , Amount = x.Count() })
          .ToList();


            return results;
        }

        public List<ExceptionAndDate> ExceptionsOnDay()
        {
            var results = _logEntryRepository.GetAllLogs
            .OrderByDescending(item => item.Date)
          .GroupBy(item => item.Exception.Count())
          .Select(x => new ExceptionAndDate() { Date = x.First().Date, NumberOfExetions = x.Count().ToString() })
          .ToList();
            return results;
        }

        public string[] GetDays()
        {
            string[] dayName = { "Mondays", "Tuesdays", "Wednesdays", "Thursdays", "Fridays", "Saturdays", "Sundays" };
            return dayName;

        }

        public void CheckAndDayExToCorrectDay()
        {
            
            if (_firstMonthDate < 10)
                _firstMonthDateString = $"0{_firstMonthDateString}";

            foreach (var item in _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != ""))
            {

                _dayOfWeekDate = new DateTime(DateTime.Now.Year, int.Parse(item.Date.KeepOnlyMonth()), int.Parse(item.Date.KeepOnlyDay()));
                _dayOfWeek = _dayOfWeekDate.DayOfWeek.ToString();

                switch (_dayOfWeek)
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

        public string GetStartDate()
        {
            return _time.WeekStart.Date.ToString();
        }

        public string GetEndDate()
        {
            return _time.WeekEnd.Date.ToString();
        }

    }

    public class LoggerKindCounter
    {
        public LogEntry Logger { get; set; }
        public int Amount { get; set; }
    }

    public class ThreadNameAndThreadCount
    {
        public string Thread { get; set; }
        public int Amount { get; set; }
    }

    public class ExceptionAndDate
    {
        public string NumberOfExetions { get; set; }
        public string Date { get; set; }
    }
}
