using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NectimaLogging.Data;
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
        private int PageCounter { get; set; } = 10;



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

            //prefix = _myServices.CheckIfLastCharIsDigit(prefix);


            //prefix = _myServices.AddWhiteSpace(prefix);

            ViewData["test"] = KeywordHighlight.HighlightKeywords(_logEntryRepository.GetAllLogs.Where(x => x.Level == "Info").ToString(),prefix);

            


            int pId;
            pId = _myServices.ParseInputToInt(prefix);
            if (prefix == null)
            {
                return RedirectToAction("Index", "Error");
            }

            prefix = _myServices.RemoveWhiteSpace(prefix);
            if (_myServices.RemoveUnWantedChars(prefix))
            {
                return RedirectToAction("Index", "Error");
            }
            if ((pId <= int.MaxValue && pId != 0 && !_logEntryRepository.IsBiggerThenMaxId(pId)))
            {
                if (_myServices.IsNumber(prefix))
                {
                    return View(_logEntryRepository.GetLogbyId(
                    int.Parse(prefix)));
                }
            }

            if (_myServices.IsLetters(prefix))
            {
                prefix = _myServices.FirstCharToUpper(prefix);
                
                return View("FilteredLogs", _logEntryRepository.GetLogByLevel(prefix));
            }
            if (_myServices.IsNumberAndLetters(prefix))
            {
                return RedirectToAction("Index", "Error");
            }
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
            
            if(levelInput == Level.Empty &&
                string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(dateInput) &&
                string.IsNullOrWhiteSpace(thread) && string.IsNullOrWhiteSpace(message))
            {
                return RedirectToAction(nameof(ResultAllLogs),_logEntryRepository.GetAllLogs);
            }
           
            int pId;
            pId = _myServices.ParseInputToInt(id);

            if (_logEntryRepository.IsBiggerThenMaxId(pId) )
            {
                return RedirectToAction("Index", "Error"); 
            }
            if ((pId <= int.MaxValue && pId != 0 && !_logEntryRepository.IsBiggerThenMaxId(pId)))
            {

                if (_myServices.IsNumber(id))
                {
                    if(dateInput == null)
                    return View("SearchResult", _logEntryRepository.GetLogbyId(pId));

                    else
                    {
                        return View("FilteredLogs", _logEntryRepository.GetLogByDate(dateInput)
                            .Where(x => x.Id == pId));
                    }
                }
            }
            else if(pId == 0 && id != null)
            {
                return RedirectToAction("Index", "Error");
            }
            
            return View("FilteredLogs", _logEntryRepository.AdvancedSearchFilter( levelInput,  dateInput,  thread,message));

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