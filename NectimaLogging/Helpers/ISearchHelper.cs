using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Helpers
{
    public interface ISearchHelper
    {
        ILogEntryRepository LogEntryRepository { get; set; }

        Level Level { get; set; }
        string Id { get; set; }
        int IdParsed { get; set; }
        string DateInput { get; set; }
        string Thread { get; set; }
        string Message { get; set; }
    }
}
