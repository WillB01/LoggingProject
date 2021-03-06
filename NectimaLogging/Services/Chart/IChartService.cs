﻿using NectimaLogging.Models;
using NectimaLogging.Services;
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
        List<ThreadNameAndThreadCount> ThreadCounter();
        List<LoggerKindCounter> LoggerKindCounters();
        List<ExceptionAndDate> ExceptionsOnDay();
        string[] GetDays();
        double[] GetExProcentDay();
        void CheckAndDayExToCorrectDay();
        string GetStartDate();
        string GetEndDate();


    }
}
