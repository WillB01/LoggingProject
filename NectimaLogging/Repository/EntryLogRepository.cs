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
            (!string.IsNullOrWhiteSpace(thread) ? x.Thread.Equals(thread) : true) && 
            (!string.IsNullOrWhiteSpace(dateInput) ? x.Date.Split(" ")
            .First()
            .Substring(0).Equals(dateInput) : true) &&
            (levelInput != 0 ? x.Level.Equals(GetLevelBySting(levelInput)) : true) &&
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

        public IEnumerable<LogEntry> GetLogbyId(int id)
        {
            var test = GetAllLogs.Where(x =>
            (x.Id != id) ? x.Id.Equals(id) : true);

            var b = test.Where(x => (x.Id == 0) ? x.Id.Equals(id) : true);

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

        public bool IsIdOkey(int id)
        {

            if (IsBiggerThenMaxId(id))
            {
                return false;
            }
            else if ((id <= int.MaxValue && id != 0 && !IsBiggerThenMaxId(id)))
            {
                return true;
            }
            return false;
        }




    }
}
