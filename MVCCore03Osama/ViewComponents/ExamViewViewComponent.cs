using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;
using University.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace University.ViewComponents
{
    public class ExamViewViewComponent : ViewComponent
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationDbContext ApplicationDbContext;
        public ExamViewViewComponent(UserManager<ApplicationUser> UserManager
                            , ApplicationDbContext _ApplicationDbContext)
        {
            this.userManager = UserManager;
            ApplicationDbContext = _ApplicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var url = Request.GetDisplayUrl();
            var x = int.Parse(url.Split("/")[^1]);

            var _user = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.stuId = _user.Id;

            ViewBag.CourseId = x;



            var Choice = ApplicationDbContext.Choice.ToList();
            ViewBag.Choices = Choice;
            return View(ApplicationDbContext.Question.Where(q => q.CourseId == x).ToList());
        }
    }
}
