using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewModels
{
    public class LogListViewModel
    {
        public IEnumerable<LogEntry> Logs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
