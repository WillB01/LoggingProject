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
        IEnumerable<LogEntry> GetLogByDate(string inputDate);
        IEnumerable<LogEntry> GetLogByLevel(string level);
        IEnumerable<LogEntry> AdvancedSearchFilter(Level levelInput, string dateInput, string thread, string message);

        string[] WordFilter(string s);
        bool IsBiggerThenMaxId(int id);

        bool IsIdOkey(int id);


        IEnumerable<LogEntry> GetLogbyId(int id);


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
