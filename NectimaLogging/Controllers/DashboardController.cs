using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Interface;
using NectimaLogging.Models;
using NectimaLogging.Repository;
using NectimaLogging.Services.Chart;
using NectimaLogging.Services.Chart.ExceptionChart;

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
            var start = new ExceptionChartStart(_chartService, _week, 0, false, false);
            start.Run();

            return View(start);
                      
         
        }
        [HttpPost]
        public IActionResult Index(bool isPrev, bool isNext, int prevWeek)
        {

            var start = new ExceptionChartStart(_chartService, _week, prevWeek, isPrev, isNext);
            start.Run();

            return View(start);


        }
    }









}