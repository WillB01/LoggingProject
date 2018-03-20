using NectimaLogging.Data;
using NectimaLogging.Models;
using NectimaLogging.Services;
using NectimaLogging.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NectimaLogging.Repository
{
    public class EntryLogRepository : ILogEntryRepository
    {
        
        private AppDbContext _appDb;
        private LogEntry _logEntry;
        private IMyServices _myServices;
        private LogListViewModel _logListViewModel;

        public IEnumerable<LogEntry> MyLevel { get; set; }
        

        


        public EntryLogRepository(AppDbContext appDb, IMyServices myServices)
        {
            _appDb = appDb;
            _logEntry = new LogEntry();
            _myServices = myServices;
            _logListViewModel = new LogListViewModel();
        }
        public IEnumerable<LogEntry> GetAllLogs => _appDb.LogEntries;

        public IEnumerable<LogEntry> GetLogByLevelAndDate(string inputDate, Level level)
        {
            switch (level)
            {
                case Level.Debug:
                    MyLevel = GetLogByLevel("Debug");
                    break;
                case Level.Error:
                    MyLevel = GetLogByLevel("Error");
                    break;
                case Level.Fatal:
                    MyLevel = GetLogByLevel("Fatal");
                    break;
                case Level.Info:
                    MyLevel = GetLogByLevel("Info");
                    break;
                case Level.Off:
                    MyLevel = GetLogByLevel("Off");
                    break;
                case Level.Warn:
                    MyLevel = GetLogByLevel("Warn");
                    break;
                default:
                    break;
            }
            if(inputDate == null)
            {
                return MyLevel;
            }
            
            
                return MyLevel.Where(x => x.Date
                .Split(" ")
                .First()
                .Substring(0) == inputDate);
            
           
           
            

            

            //if(level == Level.Debug || level == Level.Error || level == Level.Fatal || level == Level.Info
            //    || level == Level.Off || level == Level.Warn)
            //{

            //}
        }

        public IEnumerable<LogEntry> GetLogByDate(string inputDate)
        {
            var b = GetAllLogs.Where(x => x.Date
           .Split(" ")
           .First()
           .Substring(0) == inputDate);

            return b;
        }

















        public string[] WordFilter(string s)
        {
            return Regex.Split(s, @"\W+");
        }


        public IEnumerable<LogEntry> GetLogByLevel(string level)
        {
            string[] searchFilter = WordFilter(level);
            var theLevel = "";
            foreach (var item in searchFilter)
            {
                if (item == "Info" || item == "Debug" || item == "Info" || item == "Warn" ||
                    item == "Error" || item == "Fatal" || item == "Off")
                {
                    theLevel = item;
                }

            }

            var log = GetAllLogs.Where(x => x.Level == theLevel);

            return log;
        }

        public LogEntry GetLogbyId(int id)
        {
    
            
            return (GetAllLogs.FirstOrDefault(x => x.Id == id));
        }

        public bool IsBiggerThenMaxId(int id)
        {
           if(id >= GetAllLogs.Count())
            {
                return true;
            }
            
            return false;
        }

        public string IsSearchSingleOrNot(string prefix)
        {
            if (_myServices.ContainsLetters(prefix))
            {
                string[] searchFilter = WordFilter(prefix);
                foreach (var item in searchFilter)
                {
                    var test = GetLogbyId(int.Parse(item));
                    if (item == test.Id.ToString())
                    {
                        return item;
                    }
                }
            }


            return "";


        }

        public IEnumerable<AutoSearchRepository> AutoSearchRepositories(string input, int dropdownAmountofItems)
        {
            var logs = (from c in GetAllLogs
                        where (c.Level.StartsWith(input) || c.Id.ToString().StartsWith(input))

                        select new AutoSearchRepository { value = $"{c.Id} {c.Level} {c.Exception} {c.Date}" });



            var resizedLogs = logs.TakeLast(dropdownAmountofItems);
            return resizedLogs.OrderByDescending(x => x.value);

        }


        //public IEnumerable<object> AmoutOfLogs(int amount, IEnumerable<object>logs)
        //{
        //    var resizedLogs = logs.Take(amount).OrderByDescending(x);

        //    return resizedLogs.OrderByDescending(x => x.);
        //}
    }
}
