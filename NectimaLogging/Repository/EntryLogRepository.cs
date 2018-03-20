using NectimaLogging.Data;
using NectimaLogging.Models;
using NectimaLogging.Services;
using System;
using System.Collections.Generic;
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

        public EntryLogRepository(AppDbContext appDb, IMyServices myServices)
        {
            _appDb = appDb;
            _logEntry = new LogEntry();
            _myServices = myServices;
        }
        public IEnumerable<LogEntry> GetAllLogs => _appDb.LogEntries;
        
        public LogEntry GetLogByDate(string inputDate)
        {
            return GetAllLogs.First(x => x.Date
           .Split(" ")
           .First()
           .Substring(0) == inputDate);
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
                if (item == "Info" || item == "Deboug" || item == "Info" || item == "Warn" ||
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
