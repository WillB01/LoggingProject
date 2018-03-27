using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NectimaLogging.Data;
using NectimaLogging.Helpers;
using NectimaLogging.Models;
using NectimaLogging.Services;
using NectimaLogging.ViewModels;
using NLog;
using ReflectionIT.Mvc.Paging;

namespace NectimaLogging.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private ILogEntryRepository _logEntryRepository;
        private IMyServices _myServices;
        private int PageSize { get; set; } = 4;

        public HomeController(ILogger<HomeController> logger,
            ILogEntryRepository logEntryRepository, IMyServices myServices)
        {
            _logger = logger;
            _logEntryRepository = logEntryRepository;
            _myServices = myServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
   
        [HttpGet]

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult FilteredLogs()
        {
            return View();
        }

        
        [HttpGet]
        [Route("search-results")]
        public IActionResult AdvancedSearchResult(Level levelInput, string id, string dateInput, string thread, string message, string prefix)
        {

            var search = new SearchFilterLogic(_logEntryRepository, _myServices, levelInput, id, dateInput, thread, message,prefix);
            if (search.SearchFilter() == true)
            {
                return View("FilteredLogs", search.FilteredLogs());
            }
            else if (search.SearchFilter() == false)
            {
                return RedirectToAction("Search");
            }

            return RedirectToAction("FilteredLogs", _logEntryRepository.AdvancedSearchFilter(levelInput, dateInput, thread, message));
        }

        [Route("all-logs")]
        public IActionResult ResultAllLogs(int logPage = 1)
        {
            return View(new LogListViewModel
            {

                Logs = _logEntryRepository.GetAllLogs
                 .OrderBy(p => p.Id)
                 .Skip((logPage - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    AddMorePages = logPage,
                    CurrentPage = logPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _logEntryRepository.GetAllLogs.Count()
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResultAllLogs(int next, int previous, bool isNext, bool isPrevious, int addP)
        {
            var paging = new PagingLogic(next, previous, isNext, isPrevious, addP, PageSize, _logEntryRepository);

            return View(paging.LogListViewModel());
        }

    }
}