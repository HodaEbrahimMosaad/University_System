using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        public AdminController(IStudent _student,IInstructor _instructor)
        {
            student = _student;
            instructor = _instructor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getActiveStudents()
        {
            
           
            return View(await student.getAllActiveStudents());
        }
        public  bool AssignCourseToStudent( string stdID, int CrsID)
        {
            bool isAssgned = student.regesterStudentInCourse(stdID, CrsID);
            return isAssgned;
            
        }
        public bool RemoveCourseToStudent(string stdID, int CrsID)
        {
            bool isRemoved = student.removeStudentFromCourse(stdID, CrsID);
            return isRemoved;

        }
        //===================================================//


        public async Task<IActionResult> getActiveInstructor()
        {


            return View(await instructor.getAllActiveInstructors());
        }
        public bool AssignCourseToInstructor(string InsID, int CrsID)
        {
            bool isAssgned = instructor.regesterInstructorInCourse(InsID, CrsID);
            return isAssgned;

        }
        public bool RemoveCourseToInstructor(string insID, int CrsID)
        {
            bool isRemoved = instructor.removeInstructorFromCourse(insID, CrsID);
            return isRemoved;

        }
    }
}
