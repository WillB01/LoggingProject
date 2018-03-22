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




            //return GetAllLogs.Where(l => 
            //(!string.IsNullOrWhiteSpace(dateInput) || l.Date == dateInput) 
            //&&
            //(!string.IsNullOrWhiteSpace(thread) || l.Thread.Contains(thread)) 
            //&&
            //(levelInput == 0 || l.Level == GetLevelBySting(levelInput)));

           

            //if (levelInput == 0 && dateInput == null && thread == null)
            //{
            //    return MyLevel;
            //}
            //if(dateInput != null && levelInput == 0 && thread == null) // return logs by date
            //{
            //    return GetLogByDate(dateInput);
            //}
            //if(dateInput == null && levelInput != 0 && thread == null) // return logs by level
            //{
            //    return GetLogsByLevelByEnum(levelInput);
            //}
            //if (dateInput == null && levelInput == 0 && thread != null) // return logs by thread
            //{

            //    return GetLogsByThread(thread);
            //}
            //if(dateInput != null || levelInput != 0 || thread != null)
            //{
            //    if (dateInput == null && levelInput != 0 && thread != null) //return logs by level and then Thread.
            //    {
                    
            //        var searchLevelThenThread = GetLogsByLevelByEnum(levelInput) 
            //            .Where(x => x.Thread == thread);
                    
            //        return searchLevelThenThread;
            //    }
            //    if(dateInput != null && levelInput != 0 && thread == null) // return logs by Date and then Level.
            //    { var searchDateLevel = GetLogByDate(dateInput)
            //          .Where(x => x.Level == GetLevelBySting(levelInput));
            //        return searchDateLevel;
            //    }
            //    else if(dateInput != null && levelInput == 0 && thread != null) // return logs by Date and then Thread.
            //    {
            //        var searchDateThread = GetLogByDate(dateInput)
            //            .Where(x => x.Thread == thread);
            //        return searchDateThread;
            //    }
            //    else                                                           // return logs by Date and Level and thread.
            //    {
            //        var searchDateLevelThread = GetLogByDate(dateInput)
            //            .Where(x => x.Level == GetLevelBySting(levelInput))
            //            .Where(i => i.Thread == thread);
            //        return searchDateLevelThread;
            //    }
                
                
            //}
            //else if(dateInput == null && levelInput == 0)
            //{
            //    MyLevel = GetLogsByThread(thread);
            //    return MyLevel;
            //}
            //return MyLevel;
            
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
            //Func<bool> isIdToBig = () => {
            //    foreach (var item in GetAllLogs)
            //        if (item.Id != id)
            //            return true;
            //    return false;
            //};
            //if (isIdToBig())
            //{
            //    return (GetAllLogs.FirstOrDefault(x => x.Id == id));
            //}
            
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

                        select new AutoSearchRepository { value = $"{c.Level}" });



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
