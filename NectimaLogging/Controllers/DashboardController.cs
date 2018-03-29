using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Interface;
using NectimaLogging.Models;
using NectimaLogging.Repository;
using NectimaLogging.Services.Chart;

namespace NectimaLogging.Controllers
{
    public class DashboardController : Controller
    {
        private ILogEntryRepository _logEntryRepository;
        private IChartService _chartService;
        private IWeek _week;
        private IDateAndExcetionsRepository _DateAndExcetions;


        public DashboardController(ILogEntryRepository logEntryRepository, IChartService chartService, IWeek week, IDateAndExcetionsRepository dateAndExcetionsRepository)
        {
            _logEntryRepository = logEntryRepository;
            _chartService = chartService;
            _week = week;
            _DateAndExcetions = dateAndExcetionsRepository;
        }

        public IActionResult Index()
        {
            _week.PrevWeek = 20;
            
            
            var start = new ExceptionChartStart(_chartService, _week, 0);         
            return View(start.Start(7));
                      
         
        }
        [HttpPost]
        public IActionResult Index(int prevWeek)
        {
            var start = new ExceptionChartStart(_chartService, _week, 7);
            
            return View(start.Start(7));


        }
    }









}