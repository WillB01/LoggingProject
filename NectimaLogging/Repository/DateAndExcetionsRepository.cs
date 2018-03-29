using System.Collections.Generic;
using NectimaLogging.Controllers;

namespace NectimaLogging.Repository
{
    public class DateAndExcetionsRepository : IDateAndExcetionsRepository
    {
        public int AmountOfExceptions { get; set; }
        public int PrevWeek { get; set; }
        public string Date { get; set; }
        
        public IEnumerable<WeekEntry> AddPrevWeek { get; set ; }
    }









}