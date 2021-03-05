using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using University.Data;
using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace University.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AllUsersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public AllUsersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }
        //Details
        [NoDirectAccess]
        public async Task<IActionResult> Details(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var user = await userManager.Users
                .FirstOrDefaultAsync(u=>u.Id.Equals(id));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        //approve
        [NoDirectAccess]
        public async Task<IActionResult> Approve(string id)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id,  int x=0)
        {
            ApplicationUser myuser = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (!id.Equals(myuser.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    myuser.status = Status.Active;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllUsers", userManager.Users.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Approve", myuser) });
        }
        //
        //[HttpPost]
        public async Task<IActionResult> ChangeStatus(string id, int status)
        {
            ApplicationUser myuser = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(myuser!=null)
            {
                switch (status)
                {
                    case 0:
                        myuser.status = Status.Active;
                        if(myuser.UserRole==Role.Instructor)
                            await userManager.AddToRoleAsync(myuser, "Instructor");
                        else if (myuser.UserRole == Role.Student)
                            await userManager.AddToRoleAsync(myuser, "Student");
                        else if (myuser.UserRole == Role.Admin)
                            await userManager.AddToRoleAsync(myuser, "Admin");

                        break;
                    case 1:
                        myuser.status = Status.Pendding;
                        break;
                    case 2:
                        myuser.status = Status.Blocked;
                        break;
                }
                
                db.SaveChanges();
            }
            //await UserManager_.AddToRoleAsync(Ins, "Instructor");
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllUsers", userManager.Users.ToList()) });
        }
    }
}
