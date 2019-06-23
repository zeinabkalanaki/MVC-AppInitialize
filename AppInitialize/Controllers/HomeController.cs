using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppInitialize.Controllers
{
    public class HomeController : Controller
    {
        MyUptimeMeasure _myUptimeMeasure;
        public HomeController(MyUptimeMeasure myUptimeMeasure)
        {
            _myUptimeMeasure = myUptimeMeasure;
        }
        public IActionResult Index()
        {
            return View(_myUptimeMeasure.GetUptime());
        }
    }
}
