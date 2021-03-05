using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewComponents
{
    public class GraphsViewComponent : ViewComponent
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        public GraphsViewComponent(IStudent _student, IInstructor _instructor, ICourse _course)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var st = await student.getAllStudents();
            var ins = await instructor.getAllActiveInstructors();
            var co = await course.GetAll();
            ViewBag.studentsCount = st.Count;
            ViewBag.instructorsCount = ins.Count;
            ViewBag.coursesCount = co.Count;
            
            return View();
        }
    }
}
