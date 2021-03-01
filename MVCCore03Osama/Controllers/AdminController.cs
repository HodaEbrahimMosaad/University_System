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
        private readonly ICourse course;
        public AdminController(IStudent _student,IInstructor _instructor, ICourse _course)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
        }
        //[Authorize(Roles = "Admin"), Authorize(Roles = "Instructor"), Authorize(Roles = "Student")]
        public async Task<IActionResult> AdminHome()
        {
            var st = await student.getAllStudents();
            var ins = await instructor.getAllActiveInstructors();
            var co = await course.GetAll();
            ViewBag.studentsCount = st.Count;
            ViewBag.instructorsCount = ins.Count;
            ViewBag.coursesCount = co.Count;
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
