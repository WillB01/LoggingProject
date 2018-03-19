using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewComponents
{
    public class LogsViewComponent : ViewComponent
    {
        private readonly ILogEntryRepository _logEntryRepository;

        public LogsViewComponent(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }

        public IViewComponentResult Invoke()
        {
            
            return  View("Default");

        }
    }
}
