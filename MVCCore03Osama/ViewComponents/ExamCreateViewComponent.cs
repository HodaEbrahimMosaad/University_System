using Microsoft.AspNetCore.Http.Extensions;
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
    public class ExamCreateViewComponent : ViewComponent
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        public ApplicationDbContext ApplicationDbContext;
        public ExamCreateViewComponent(IStudent _student, IInstructor _instructor, ICourse _course, ApplicationDbContext _ApplicationDbContext)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
            ApplicationDbContext = _ApplicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var url = Request.GetDisplayUrl();
            ViewBag.CourseId = int.Parse(url.Split("/")[^1]);
            return View();
        }
    }
}
