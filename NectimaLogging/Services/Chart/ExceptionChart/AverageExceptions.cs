using NectimaLogging.Controllers;
using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NectimaLogging.Services.Chart
{
    public class AverageExceptions
    {
        public int First { get; set; } = 1;
        public double DaysInMonth { get; set; }
        public double AverageWeek => Exceptions / WeeksInLogLifeTime();
        public double Exceptions { get; set; }
        public string AverageWeekPrint { get; set; }
        public int WeekSumNum { get; private set; }
        public LogEntry FirstMonthLog => _logEntryRepository.GetAllLogs.First(x => x.Id == First);
        public string LastMonthLog => _logEntryRepository.GetAllLogs.Last().Date;
        public int FirstMonthDate => int.Parse(FirstMonthLog.Date.KeepOnlyMonth());
        public int LastMonthDate => int.Parse(LastMonthLog.KeepOnlyMonth());
        public DateTime FirstLog => new DateTime(DateTime.Today.Year, FirstMonthDate, First);
        public DateTime StartOfFirstWeek => FirstLog.AddDays(First - (int)(FirstLog.DayOfWeek));
        public List<string> DaysInWeekWithException { get; set; }

        public DateTime WeekStart { get; private set; }
        public DateTime WeekEnd { get; private set; }




        private ILogEntryRepository _logEntryRepository;

        public AverageExceptions(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
            WeeksInLogLifeTime();


        }

        public string GetNewFormatMonth()
        {

            AverageWeekPrint = AverageWeek.ToString("N");
            AverageWeekPrint = AverageWeekPrint.Replace(',', '.');

            return AverageWeekPrint;
        }

        public int WeeksInLogLifeTime()
        {     
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new
                    {
                        weekStart = StartOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Month <= LastMonthDate)
                    .Select(x => new
                    {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(4)
                    })
                    .SkipWhile(x => x.weekFinish < FirstLog.AddDays(1))
                    .Select((x, i) => new
                    {
                        x.weekStart,
                        x.weekFinish,
                        weekNum = i + 1
                    });

            foreach (var item in weeks)
            {
                WeekSumNum = item.weekNum;
                WeekStart = item.weekStart;
                WeekEnd = item.weekFinish;
            }

            return WeekSumNum;
        }
    }
}



