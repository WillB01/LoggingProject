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

        public IViewComponentResult Invoke(string Prefix)
        {

            var log = (from c in _logEntryRepository.GetAllLogs
                        where c.Level.StartsWith(Prefix)
                        select new { value = c.Level });



            return View("Default", _logEntryRepository
                .GetAllLogs.FirstOrDefault(x => x.Level == Prefix));

        }
    }
}
