using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewComponents
{
    public class ShowGradesViewComponent : ViewComponent
    {
        public ShowGradesViewComponent(ApplicationDbContext _ApplicationDbContext,
            UserManager<ApplicationUser> UserManager)
        {
            this.ApplicationDbContext = _ApplicationDbContext;
            this.UserManager = UserManager;
        }

        public ApplicationDbContext ApplicationDbContext { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public async Task<IViewComponentResult> InvokeAsync() { 

            var url = Request.GetDisplayUrl();
            var crsid = int.Parse(url.Split("/")[^1]);
            

            var _user = await UserManager.FindByNameAsync(User.Identity.Name);

            ViewBag.stuId = _user.Id;

            var users = ApplicationDbContext.studentCourses.
                Where(sc=> sc.CourseId == crsid).Select(sc => sc.StudentId).ToList();

            var students = ApplicationDbContext.Users
                .Where(s => users.Contains(s.Id));
            ViewBag.courseid = crsid;

            return View(students);
        }
    }
}
