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
        private const string ID = "id";
        private const string LEVEL = "id";
        private const string ERROR = "error";

        private int PageSize { get; set; } = 4;
        private int _parsedId;

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

        //[HttpPost]
        //public IActionResult SearchResult(string prefix)
        //{
          
          
        //    var searchHelper = new SearchHelper(prefix, _myServices, _logEntryRepository);
        //    if(searchHelper.SeachHelper() == ERROR)
        //    {
        //        return RedirectToAction("Index", "Error");
        //    }
        //    else if(searchHelper.SeachHelper() == LEVEL)
        //    {
        //        return View("FilteredLogs", searchHelper.ReturnSeveralLogs());
        //    }
           
     
        //    return RedirectToAction("Index", "Error");

        //}

        public IActionResult FilteredLogs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdvancedSearchResult(Level levelInput, string id, string dateInput, string thread, string message, string prefix) //ID AND DATE TOGETHER NEEDS TODO
        {


            var b = new AdvSearchHelper(_logEntryRepository, _myServices, levelInput, id, dateInput, thread, message,prefix);
            if (b.SearchFilter() == true)
            {
                return View("FilteredLogs", b.FilteredLogs());
            }
            else if (b.SearchFilter() == false)
            {
                return RedirectToAction("Index", "Error");
            }

            //if (levelInput == Level.Empty &&
            //    string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(dateInput) &&
            //    string.IsNullOrWhiteSpace(thread) && string.IsNullOrWhiteSpace(message))
            //{
            //    return RedirectToAction(nameof(ResultAllLogs), _logEntryRepository.GetAllLogs);
            //}
            
            //_parsedId = _myServices.ParseInputToInt(id);

            //if (_logEntryRepository.IsBiggerThenMaxId(_parsedId)) 
            //{
            //    return RedirectToAction("Index", "Error");
            //}
            //if (_logEntryRepository.IsIdOkey(_parsedId) && !string.IsNullOrWhiteSpace(id)/*(_parsedId <= int.MaxValue && _parsedId != 0 && !_logEntryRepository.IsBiggerThenMaxId(_parsedId))*/)
            //{

            //    if (_myServices.IsNumber(id))
            //    {
            //        if (dateInput == null)
            //        {
            //            var getlogsById = _logEntryRepository.GetLogbyId(_parsedId);
            //            if (getlogsById == null)
            //            {
            //                return RedirectToAction("Index", "Error");
            //            }
            //            return View("FilteredLogs", getlogsById);
            //        }
                   
            //        else
            //        {
            //            return View("FilteredLogs", _logEntryRepository.GetLogByDate(dateInput)
            //                .Where(x => x.Id == _parsedId));
            //        }
            //    }
            //}
            //else if (_parsedId == 0 && id != null)
            //{
            //    return RedirectToAction("Index", "Error");
            //}

            return View("FilteredLogs", _logEntryRepository.AdvancedSearchFilter(levelInput, dateInput, thread, message));
        }

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
        public IActionResult ResultAllLogs(int next, int previous, bool isNext, bool isPrevious, int addP)
        {
            var paging = new PagingLogic(next, previous, isNext, isPrevious, addP, PageSize, _logEntryRepository);

            return View(paging.LogListViewModel());
        }

    }
}