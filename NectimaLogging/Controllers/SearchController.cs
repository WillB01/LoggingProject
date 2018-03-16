using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NectimaLogging.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}