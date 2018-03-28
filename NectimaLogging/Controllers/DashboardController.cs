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
        private IWeek _week;
        

        public DashboardController(ILogEntryRepository logEntryRepository, IChartService chartService, IWeek week)
        {
            _logEntryRepository = logEntryRepository;
            _chartService = chartService;
            _week = week;
        }

        public List<string> Index()
        {



            return _week.GetDays();

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
        DateTime Monday { get; set; }
        DateTime Tuesday { get; set; }
        DateTime Wendsday { get; set; }
        DateTime Thursday { get; set; }
        DateTime Friday { get; set; }
        DateTime Saturday { get; set; }
        DateTime Sunday { get; set; }
        DateTime Today { get; set; }

        string MondayString { get; set; }
        string TuesdayString { get; set; }
        string WendsdayString { get; set; }
        string ThursdayString { get; set; }
        string FridayString { get; set; }
        string SaturdayString { get; set; }
        string SundayString { get; set; }
        string TodayString { get; set; }

        List<string> Days { get; set; }
        List<string> GetDays();
    }

    public class ExceptionAndDateChart : IWeek
    {
        public DateTime Monday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        public DateTime Tuesday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
        public DateTime Wendsday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
        public DateTime Thursday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
        public DateTime Friday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);
        public DateTime Saturday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
        public DateTime Sunday { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        public DateTime Today { get; set; } = DateTime.Now.Date;

        public string MondayString { get; set; }
        public string TuesdayString { get; set; }
        public string WendsdayString { get; set; }
        public string ThursdayString { get; set; }
        public string FridayString { get; set; }
        public string SaturdayString { get; set; }
        public string SundayString { get; set; }
        public string TodayString { get; set; }

        public List<string> Days { get; set; }

        public ExceptionAndDateChart()
        {
            MondayString = Monday.RemoveTime();
            TuesdayString = Tuesday.RemoveTime();
            WendsdayString = Wendsday.RemoveTime();
            ThursdayString = Thursday.RemoveTime();
            FridayString = Friday.RemoveTime();
            SaturdayString = Saturday.RemoveTime();
            SundayString = Sunday.RemoveTime();
            TodayString = Today.RemoveTime();

        }

        public List<string> GetDays()
        {
            Days = new List<string>
            {
                SundayString,
                MondayString,
                TuesdayString,
                WendsdayString,
                ThursdayString,
                FridayString,
                SaturdayString,
                TodayString
                
                
            };

           
            
            return Days;
        }
    }
}