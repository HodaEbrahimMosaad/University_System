using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Controllers
{
    public class HomeUserController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CourseId"] = Request.Query["CourseID"];
            return View();
        }
    }
}
