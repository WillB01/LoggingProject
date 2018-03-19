using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.ViewComponents
{
    public class LogViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string prefix)
        {
            return View("Default");
        }

       
    }
}
