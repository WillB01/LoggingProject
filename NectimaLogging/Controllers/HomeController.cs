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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logs(int id)
        {
            if (ModelState.IsValid)
            {
                return View(_logEntryRepository.GetLogbyId(id));

            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Search()
        {


            return View();

        }


        //[HttpPost]

        //public JsonResult Search(string prefix)
        //{
        //    prefix = _myServices.FirstCharToUpper(prefix);

        //    return Json(_logEntryRepository.AutoSearchRepositories(prefix, 20));

        //}

        [HttpPost]
        public IActionResult SearchResult(string prefix)
        {
            var searchHelper = new SearchHelper(prefix, _myServices, _logEntryRepository);
            if(searchHelper.SeachHelper() == "error")
            {
                return RedirectToAction("Index", "Error");
            }
            else if(searchHelper.SeachHelper() == "filteredLogs")
            {
                return View("FilteredLogs", searchHelper.ReturnSeveralLogs());
            }
            else if(searchHelper.SeachHelper() == "singleSearch")
            {
                return View(searchHelper.ReturnLog());
            }


            //_parsedId = _myServices.ParseInputToInt(prefix);

            //if (prefix == null)
            //{
            //    return RedirectToAction("Index", "Error");
            //}

            //prefix = _myServices.RemoveWhiteSpace(prefix);
            //if (_myServices.RemoveUnWantedChars(prefix))
            //{
            //    return RedirectToAction("Index", "Error");
            //}
            //if ((_parsedId <= int.MaxValue && _parsedId != 0 && !_logEntryRepository.IsBiggerThenMaxId(_parsedId)))
            //{
            //    if (_myServices.IsNumber(prefix))
            //    {
            //        var getlogsById = _logEntryRepository.GetLogbyId(
            //        int.Parse(prefix));

            //        if (getlogsById == null)
            //        {
            //            return RedirectToAction("Index", "Error");
            //        }
            //        return View(getlogsById);
            //    }
            //}

            //if (_myServices.IsLetters(prefix))
            //{
            //    prefix = _myServices.FirstCharToUpper(prefix);

            //    return View("FilteredLogs", _logEntryRepository.GetLogByLevel(prefix));
            //}
            //if (_myServices.IsNumberAndLetters(prefix))
            //{
            //    return RedirectToAction("Index", "Error");
            //}
            return RedirectToAction("Index", "Error");

            //prefix = _myServices.FirstCharToUpper(prefix);
            //if (_myServices.ContainsLetters(prefix))
            //{
            //    return View("FilteredLogs", _logEntryRepository.GetLogByLevel(prefix));

            //}

            //return View(_logEntryRepository.GetLogbyId(
            //    int.Parse(_logEntryRepository.IsSearchSingleOrNot(prefix))));
        }

        public IActionResult FilteredLogs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdvancedSearchResult(Level levelInput, string id, string dateInput, string thread, string message) //ID AND DATE TOGETHER NEEDS TODO
        {

            if (levelInput == Level.Empty &&
                string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(dateInput) &&
                string.IsNullOrWhiteSpace(thread) && string.IsNullOrWhiteSpace(message))
            {
                return RedirectToAction(nameof(ResultAllLogs), _logEntryRepository.GetAllLogs);
            }
            _parsedId = _myServices.ParseInputToInt(id);
 
            if (_logEntryRepository.IsBiggerThenMaxId(_parsedId))
            {
                return RedirectToAction("Index", "Error");
            }
            if ((_parsedId <= int.MaxValue && _parsedId != 0 && !_logEntryRepository.IsBiggerThenMaxId(_parsedId)))
            {

                if (_myServices.IsNumber(id))
                {
                    if (dateInput == null)
                    {
                        var getlogsById = _logEntryRepository.GetLogbyId(_parsedId);
                        if (getlogsById == null)
                        {
                            return RedirectToAction("Index", "Error");
                        }
                        return View("SearchResult", getlogsById);
                    }
                   
                    else
                    {
                        return View("FilteredLogs", _logEntryRepository.GetLogByDate(dateInput)
                            .Where(x => x.Id == _parsedId));
                    }
                }
            }
            else if (_parsedId == 0 && id != null)
            {
                return RedirectToAction("Index", "Error");
            }

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