using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Interface
{
    public interface IChartService
    {
        int AmountOfExceptions();
        IEnumerable<LogEntry> ExcetionsPerDay();
        int DebugLevelCounter();
        int ErrorLevelCounter();
        int FatalLevelCounter();
        int InfoLevelCounter();
        int OffLevelCounter();
        int WarnLevelCounter();

    }
}
