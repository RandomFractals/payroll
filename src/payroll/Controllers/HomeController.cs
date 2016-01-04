using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace payroll.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "XYZ Payroll";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact us at:";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult NotFoundError()
        {
            return View();
        }
    }
}
