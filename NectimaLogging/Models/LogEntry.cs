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
        LogEntry GetLogByDate(string inputDate);
        string IsSearchSingleOrNot(string prefix);
        IEnumerable<AutoSearchRepository> AutoSearchRepositories(string input, int dropdownAmountofItems);
        //IEnumerable<object> AmoutOfLogs(int amount, IEnumerable<object> logs);
       
    }

    public enum Level
    {
       Info,
       Deboug,
       Warn,
       Error,
       Fatal,
       Off
                
    }
}
