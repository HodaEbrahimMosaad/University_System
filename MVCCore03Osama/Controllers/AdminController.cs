using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Models;
using University.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IStudent student;
        private readonly IInstructor instructor;
        private readonly ICourse course;
        private readonly ApplicationDbContext applicationDbContext;

        public AdminController(IStudent _student,IInstructor _instructor, ICourse _course
            ,ApplicationDbContext applicationDbContext)
        {
            student = _student;
            instructor = _instructor;
            course = _course;
            this.applicationDbContext = applicationDbContext;
        }

     
        public bool ContactAdmin(string mess, string userid)
        {
            AdminContact ca = new AdminContact()
            {
                Meessage = mess,
                SendAt = DateTime.Now,
                UserID = userid
            };
            applicationDbContext.AdminContact.Add(ca);
            applicationDbContext.SaveChanges();
            return true;
        }

        [Authorize(Roles = "Admin")]
        public bool UpdateSeen()
        {
            applicationDbContext.AdminContact.
                Where(n => n.Seen == false).ToList().ForEach(x => x.Seen = true);
            applicationDbContext.SaveChanges();
            return true;

        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowAllNoti()
        {

            var notie = applicationDbContext.AdminContact.OrderByDescending(a => a.SendAt).ToList();
            return View(notie);
        }

        [Authorize(Roles = "Admin")]
        //SendResponse?sendto=1be159a1-bbda-4ffc-9003-403de5b5e868&senderAdmin=1be159a1-bbda-4ffc-9003-403de5b5e868 
        public bool SendResponse(string sendto, string senderAdmin, string Response, int notiid)
        {
            var noti = applicationDbContext.AdminContact.FirstOrDefault(ac => ac.ID == notiid);
            noti.Response = Response;
            noti.ResponserID = senderAdmin;
            applicationDbContext.AdminContact.Update(noti);
            applicationDbContext.SaveChanges();
            return true;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getActiveStudents()
        {
            
           
            return View(await student.getAllActiveStudents());
        }

        [Authorize(Roles = "Admin")]
        public  bool AssignCourseToStudent( string stdID, int CrsID)
        {
            bool isAssgned = student.regesterStudentInCourse(stdID, CrsID);
            return isAssgned;
            
        }

        [Authorize(Roles = "Admin")]
        public bool RemoveCourseToStudent(string stdID, int CrsID)
        {
            bool isRemoved = student.removeStudentFromCourse(stdID, CrsID);
            return isRemoved;

        }
        //===================================================//

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getActiveInstructor()
        {


            return View(await instructor.getAllActiveInstructors());
        }

        [Authorize(Roles = "Admin")]
        public bool AssignCourseToInstructor(string InsID, int CrsID)
        {
            bool isAssgned = instructor.regesterInstructorInCourse(InsID, CrsID);
            return isAssgned;

        }

        [Authorize(Roles = "Admin")]
        public bool RemoveCourseToInstructor(string insID, int CrsID)
        {
            bool isRemoved = instructor.removeInstructorFromCourse(insID, CrsID);
            return isRemoved;

        }
    }
}
