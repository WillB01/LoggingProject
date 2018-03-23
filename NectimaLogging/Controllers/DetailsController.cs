using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Models;

namespace NectimaLogging.Controllers
{
    public class DetailsController : Controller
    {
        private ILogEntryRepository _logEntryRepository;

        public DetailsController(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int id)
        {
            
            return View(_logEntryRepository.GetAllLogs.FirstOrDefault(x => x.Id.Equals(id)));
        }
    }
}