using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.ViewComponents
{
    public class MyCoursesViewComponent: ViewComponent
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        public MyCoursesViewComponent(IStudent _student, IInstructor _instructor, ICourse _course, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
            signInManager = _signInManager;
            userManager = _userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Course> courses = new List<Course>();
            if (signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                string role = user.UserRole.ToString();
                string id = user.Id;
                ViewBag.userID = id;
                
                if (role == "Instructor")
                {
                    courses = await instructor.GetInstructorCourses(id);
                    ViewBag.courses = await instructor.GetInstructorCourses(id);
                }
                else if (role == "Student")
                {
                    courses = await student.GetStudentCourses(id);
                    ViewBag.courses = await student.GetStudentCourses(id);
                }
            }
            
            return View(courses.ToList());
        }
    }
}
