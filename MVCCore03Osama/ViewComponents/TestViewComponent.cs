using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.ViewComponents
{
    public class TestViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Employee Emp = new Employee () { Name = "Hoda" };
            return View(Emp);
        }
    }
}
