using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using MVCCore03Osama.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCore03Osama.ViewComponents
{
    public class ExamViewViewComponent : ViewComponent
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        public ApplicationDbContext ApplicationDbContext;
        public ExamViewViewComponent(IStudent _student, IInstructor _instructor, ICourse _course, ApplicationDbContext _ApplicationDbContext)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
            ApplicationDbContext = _ApplicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var url = Request.GetDisplayUrl();
            var x = int.Parse(url.Split("/")[^1]);
            
            var CourseId = x;
            var Choice = ApplicationDbContext.Choice.ToList();
            ViewBag.Choices = Choice;
            return View(ApplicationDbContext.Question.Where(q => q.CourseId == CourseId).ToList());
        }
    }
}
