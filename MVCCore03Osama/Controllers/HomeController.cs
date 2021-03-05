using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using University.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace University.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ITemp temp { get; set; }

        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger, ITemp temp)
        {
            this.temp = temp;
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminHome()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult StudentHome()
        {
            return View();
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult InstructorHome()
        {
            return View();
        }

        public IActionResult UserHome()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole Student = new IdentityRole("Student");
            IdentityRole Instructor = new IdentityRole("Instructor");

            await roleManager.CreateAsync(admin);
            await roleManager.CreateAsync(Student);
            await roleManager.CreateAsync(Instructor);


            var adminuser = userManager.Users.FirstOrDefault(u => u.Email == "kkkhodamnb@gmail.com");
            var Studentuser = userManager.Users.FirstOrDefault(u => u.Email == "mariam@gmail.com");
            var Instructoruser = userManager.Users.FirstOrDefault(u => u.Email == "sondos@gmail.com");

            await userManager.AddToRoleAsync(adminuser, "Admin");
            //await userManager.AddToRoleAsync(Studentuser, "Student");
            //await userManager.AddToRoleAsync(Instructoruser, "Instructor");

            return View();

        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ForAdmin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
