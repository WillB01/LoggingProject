﻿using System;
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


        [HttpPost]

        public JsonResult Search(string prefix)
        {
            prefix = _myServices.FirstCharToUpper(prefix);

            return Json(_logEntryRepository.AutoSearchRepositories(prefix, 20));

        }

        [HttpPost]
        public IActionResult SearchResult(string prefix)
        {

            //prefix = _myServices.CheckIfLastCharIsDigit(prefix);


            //prefix = _myServices.AddWhiteSpace(prefix);


            if (prefix == null)
            {
                return RedirectToAction("Search");
            }

            prefix = _myServices.RemoveWhiteSpace(prefix);
            if (_myServices.RemoveUnWantedChars(prefix))
            {
                return RedirectToAction("Search");
            }

            if (_myServices.IsNumber(prefix))
            {
                return View(_logEntryRepository.GetLogbyId(
                int.Parse(prefix)));
            }
            if (_myServices.IsLetters(prefix))
            {
                prefix = _myServices.FirstCharToUpper(prefix);
                return View("FilteredLogs", _logEntryRepository.GetLogByLevel(prefix));
            }
            if (_myServices.IsNumberAndLetters(prefix))
            {
                return RedirectToAction("Search");
            }
            return RedirectToAction("Search");

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
        public string AdvancedSearchResult(string levelInput, string prefix, string dateInput)
        {

            //var b = _logEntryRepository.GetAllLogs.First(x => x.Date
            //.Split(" ")
            //.First()
            //.Substring(0) == dateInput);

            //var hej = "what sdsdsdddd".Split(" ").First()


            //.Substring(0) == "what";
            ////foreach (var item in b.Date)
            ////{
            ////    return item.ToString();   
            ////}
            var test = _logEntryRepository.GetLogByDate(dateInput).ToString();
            return test;


            //return levelInput + " " + prefix + " " + dateInput + " " + ;
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
            var b = new PagingInfo();
            if (isNext)
            {
                PageCounter += addP;
                ++next;

                return View(new LogListViewModel
                {

                    Logs = _logEntryRepository.GetAllLogs
                .OrderBy(p => p.Id)
                .Skip((next - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        AddMorePages = next,
                        IsNext = isNext,
                        CurrentPage = next,
                        ItemsPerPage = PageSize,
                        TotalItems = _logEntryRepository.GetAllLogs.Count()
                    }
                });

            }
            else if (isPrevious)
            {
                PageCounter -= addP;
                if (previous <= 2)
                    previous = 2;
                previous--;
                return View(new LogListViewModel
                {

                    Logs = _logEntryRepository.GetAllLogs
                .OrderBy(p => p.Id)
                .Skip((previous - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        AddMorePages = previous,
                        IsPrevious = isPrevious,
                        CurrentPage = previous,
                        ItemsPerPage = PageSize,
                        TotalItems = _logEntryRepository.GetAllLogs.Count()
                    }
                });

            }
            return View();

        }

        [HttpPost]
        public void IncrementCount(int productPage, string prefix)
        {
            var test = new LogListViewModel()
            {
                Logs = _logEntryRepository.GetAllLogs
                 .OrderBy(p => p.Id)
                 .Skip((productPage + 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    NextPage = ++PageCounter,
                    CurrentPage = 3,
                    ItemsPerPage = PageSize,
                    TotalItems = _logEntryRepository.GetAllLogs.Count()
                }
            };
        }
    }
}