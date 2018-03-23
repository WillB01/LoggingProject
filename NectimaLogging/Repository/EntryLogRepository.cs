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

 
        public IEnumerable<LogEntry> AdvancedSearchFilter(
            Level levelInput, string dateInput, string thread,string message) /// Filters thru adv search. not id.
        {
           
            var test = GetAllLogs.Where(x =>
            (!string.IsNullOrWhiteSpace(thread) ? x.Thread.Equals(thread) : true) 
            && 
            (!string.IsNullOrWhiteSpace(dateInput) ? x.Date.Split(" ")
            .First()
            .Substring(0).Equals(dateInput) : true)
            &&
            (levelInput != 0 ? x.Level.Equals(GetLevelBySting(levelInput)) : true)
            &&
            (!string.IsNullOrWhiteSpace(message) ? x.Message.ToLower().Contains(message.ToLower()) : true));
        
            return test;
            
        }

        public string GetLevelBySting(Level level)
        {
            string theLevel = "";
            switch (level)
            {
                case Level.Debug:
                    theLevel = "Debug";
                    break;
                case Level.Error:
                    theLevel = "Error";
                    break;
                case Level.Fatal:
                    theLevel = "Fatal";
                    break;
                case Level.Info:
                    theLevel = "Info";
                    break;
                case Level.Off:
                    theLevel = "Off";
                    break;
                case Level.Warn:
                    theLevel = "Warn";
                    break;
                default:
                    break;
            }
            return theLevel;
        }


        public IEnumerable<LogEntry> GetLogsByLevelByEnum(Level level)
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
            return MyLevel;
        }

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
        }

        public IEnumerable<LogEntry> GetLogByDate(string inputDate)
        {
            var b = GetAllLogs.Where(x => x.Date
           .Split(" ")
           .First()
           .Substring(0) == inputDate);

            return b;
        }

        public string DateFormat(string inputDate)
        {
            return inputDate.Split(" ").First().Substring(0);
        }

        public IEnumerable<LogEntry> GetLogsByThread(string thread)
        {
         
            return GetAllLogs.Where(t => t.Thread == thread);

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


        public bool TestingCheckId(int id)
        {
            Func<bool> isIdToBig = () => {
                foreach (var item in GetAllLogs)
                    if (item.Id - 1 != id)
                        return true;
                return false;
            };
            if (isIdToBig())
            {
                return true;
            }
            return false;
        }
        

        public LogEntry GetLogbyId(int id)
        {
            var test = GetAllLogs.Where(x =>
            (x.Id != id) ? x.Id.Equals(id) : true);

            var b = test.FirstOrDefault(x => (x.Id == 0) ? x.Id.Equals(id) : true);

          
            //return (GetAllLogs.FirstOrDefault(x => x.Id == id));
            return b ;
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

                        select new AutoSearchRepository { value = $"{c.Level}" });



            var resizedLogs = logs.TakeLast(dropdownAmountofItems);
            return resizedLogs.OrderByDescending(x => x.value);

        }
    }
}
