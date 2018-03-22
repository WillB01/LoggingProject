using NectimaLogging.Services;
using NectimaLogging.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Models
{
    public class LogEntry
    {
        public string CallSite { get; set; }
        public string Date { get; set; }
        public string Exception { get; set; }
        public int Id { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string MachineName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Thread { get; set; }
        public string Username { get; set; }
    }

    public interface ILogEntryRepository
    {
        IEnumerable<LogEntry> GetAllLogs { get; }

        string[] WordFilter(string s);
        IEnumerable<LogEntry> GetLogByLevel(string level);
        LogEntry GetLogbyId(int id);
        IEnumerable<LogEntry> GetLogByDate(string inputDate);
        IEnumerable<LogEntry> GetLogByLevelAndDate(string inputDate, Level level);
        IEnumerable<LogEntry> GetLogsByThread(string thread);
        IEnumerable<LogEntry> GetLogsByLevelByEnum(Level level);
        string IsSearchSingleOrNot(string prefix);
        bool IsBiggerThenMaxId(int id);
        IEnumerable<AutoSearchRepository> AutoSearchRepositories(string input, int dropdownAmountofItems);

        //IEnumerable<object> AmoutOfLogs(int amount, IEnumerable<object> logs);

        IEnumerable<LogEntry> AdvancedSearchFilter(Level levelInput, string dateInput, string thread, string message);



        bool TestingCheckId(int id);

    }

    
    public enum Level
    {
    
       Empty, 
       Info,
       Debug,
       Warn,
       Error,
       Fatal,
       Off
                
    }
   

}
