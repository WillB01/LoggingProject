using NectimaLogging.Controllers;
using NectimaLogging.Helpers;
using NectimaLogging.Interface;
using NectimaLogging.Models;
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


        public ChartService(ILogEntryRepository logEntryRepository, IWeek week)
        {
            _logEntryRepository = logEntryRepository;
            _week = week;
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
