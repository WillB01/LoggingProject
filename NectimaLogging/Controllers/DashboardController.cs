using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NectimaLogging.Interface;
using NectimaLogging.Models;
using NectimaLogging.Repository;

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

           
            List<DateAndExcetionsRepository> test = new List<DateAndExcetionsRepository>()
            {
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SevenDaysFromTodayString),
                    Date = _week.SevenDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.SixDaysFromTodayString),
                    Date = _week.SixDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FiveDaysFromTodayString),
                    Date = _week.FiveDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.FourDaysFromTodayString),
                    Date = _week.FourDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.ThreeDaysFromTodayString),
                    Date = _week.ThreeDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TwoDaysFromTodayString),
                    Date = _week.TwoDaysFromTodayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.YesterdayString),
                    Date = _week.YesterdayString
                },
                new DateAndExcetionsRepository()
                { AmountOfExceptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" &&
                  p.Date.RemoveTime() == _week.TodayString),
                    Date = _week.TodayString
                },
            };

            ViewData["yesterday"] = test.ElementAt(6).Date;
            return View(test);
           
            
            

            

            //var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            //var tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            //var wendsday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
            //var thursday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
            //var friday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);
            //var saturday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
            //var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            //string mondayString = "2018-03-13";
            //string tuesdayString = tuesday.RemoveTime();
            //string wendsdayString = wendsday.RemoveTime();
            //string thursdayString = thursday.RemoveTime();
            //string fridayString = friday.RemoveTime();
            //string saturdayString = saturday.RemoveTime();
            //string sundayString = sunday.RemoveTime();

            //var today = DateTime.Now.Date;
            //string todayString = today.RemoveTime();


            ////int counter = 1;

            //List<string> dates = new List<string>();
            ////foreach (var item in _chartService.ExcetionsPerDay())
            ////{
            ////    elements.Add(item.Date); 
            ////}
            //var getDate = _chartService.ExcetionsPerDay().Where(p => p.Date != "");
            //string printDate = "2018-22-43";

            //foreach (var item in getDate)
            //{
            //    dates.Add(item.Date.Split(" ").First().Substring(0));

            //}
            //for (int i = 0; i < getDate.Count(); i++)
            //{

            //}



            //string amountOfExeptions = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" && 
            //                p.Date.Split(" ").First().Substring(0) == todayString).ToString();

            //string amountOfeXceptionsMonday = _chartService.ExcetionsPerDay().Count(p => p.Exception != "" && 
            //p.Date.RemoveTime() == mondayString).ToString();

            //ViewData["Monday"] = mondayString;
            //ViewData["Tuesday"] = tuesdayString;
            //ViewData["Today"] = todayString;
            //ViewData["Sunday"] = sundayString;
            //ViewData["test"] = amountOfExeptions;

            //ViewData["excetionsTotal"] = amountOfExeptions;
            //ViewData["excetionsTotalMonday"] = amountOfeXceptionsMonday;
            //return View();
        }
    }

    public interface IWeek
    {
        DateTime SevenDaysFromToday { get; set; }
        DateTime SixDaysFromToday { get; set; }
        DateTime FiveDaysFromToday { get; set; }
        DateTime FourDaysFromToday { get; set; }
        DateTime ThreeDaysFromToday { get; set; }
        DateTime TwoDaysFromToday { get; set; }
        DateTime Yesterday { get; set; }
        DateTime Today { get; set; }

        string SevenDaysFromTodayString { get; set; }
        string SixDaysFromTodayString { get; set; }
        string FiveDaysFromTodayString { get; set; }
        string FourDaysFromTodayString { get; set; }
        string ThreeDaysFromTodayString { get; set; }
        string TwoDaysFromTodayString { get; set; }
        string YesterdayString { get; set; }
        string TodayString { get; set; }

        List<string> Days { get; set; }
        List<string> GetDays();
    }

    public class ExceptionAndDateChart : IWeek
    {
        public DateTime SevenDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 3);
        public DateTime SixDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek -2);
        public DateTime FiveDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek -1);
        public DateTime FourDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        public DateTime ThreeDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
        public DateTime TwoDaysFromToday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2);
        public DateTime Yesterday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3);
        public DateTime Today { get; set; } = DateTime.Now.Date;

        public string SevenDaysFromTodayString { get; set; }
        public string SixDaysFromTodayString { get; set; }
        public string FiveDaysFromTodayString { get; set; }
        public string FourDaysFromTodayString { get; set; }
        public string ThreeDaysFromTodayString { get; set; }
        public string TwoDaysFromTodayString { get; set; }
        public string YesterdayString { get; set; }
        public string TodayString { get; set; }

        public List<string> Days { get; set; }



        public ExceptionAndDateChart()
        {
            SevenDaysFromTodayString = SevenDaysFromToday.RemoveTime();
            SixDaysFromTodayString = SixDaysFromToday.RemoveTime();
            FiveDaysFromTodayString = FiveDaysFromToday.RemoveTime();
            FourDaysFromTodayString = FourDaysFromToday.RemoveTime();
            ThreeDaysFromTodayString = ThreeDaysFromToday.RemoveTime();
            TwoDaysFromTodayString = TwoDaysFromToday.RemoveTime();
            YesterdayString = Yesterday.RemoveTime();
            TodayString = Today.RemoveTime();

        }

        public List<string> GetDays()
        {
            Days = new List<string>
            {

                SevenDaysFromTodayString,
                SixDaysFromTodayString,
                FiveDaysFromTodayString,
                FourDaysFromTodayString,
                ThreeDaysFromTodayString,
                TwoDaysFromTodayString,
                YesterdayString,
                TodayString
  
            };

           
            
            return Days;
        }

    }

    public interface IDateAndExcetionsRepository
    {
        int AmountOfExceptions { get; set; }
        string Date { get; set; }
     


    }









}