using NectimaLogging.Repository;
using NectimaLogging.Services.Chart;
using NectimaLogging.Services.Chart.ExceptionChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewModels
{
    public class ExceptionChartViewModel
    {
        public DateAndExcetionsRepository DateAndExcetionsRepository { get; set; }
        public MyCounter MyProperty { get; set; }
    }
}
