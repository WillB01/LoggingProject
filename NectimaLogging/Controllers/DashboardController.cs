using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Interface;
using NectimaLogging.Models;

namespace NectimaLogging.Controllers
{
    public class DashboardController : Controller
    {
        private ILogEntryRepository _logEntryRepository;
        private IChartService _chartService;

        public DashboardController(ILogEntryRepository logEntryRepository, IChartService chartService)
        {
            _logEntryRepository = logEntryRepository;
            _chartService = chartService;
        }

        public IActionResult Index()
        {
           

            ViewData["excetionsTotal"] = _chartService.AmountOfExceptions();
            return View();
        }
    }
}