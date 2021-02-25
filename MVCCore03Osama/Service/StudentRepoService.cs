using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.ViewModel;
using Newtonsoft.Json;

namespace MVCCore03Osama.Service
{
    public class StudentRepoService: IStudent
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public StudentRepoService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> _userManager)
        {
            db = applicationDbContext;
            userManager = _userManager;
        }
        public async Task<List<StudentCoursesVM>>getAllActiveStudents()
        {

            var students= await userManager.Users.Where
                (u => u.status == Status.Active && u.UserRole==Role.Student).ToListAsync();
            List<StudentCoursesVM> studentCourses = new List<StudentCoursesVM>();
            
            for(int i = 0; i < students?.Count(); i++)
            {
                
                var serializedParent = JsonConvert.SerializeObject(students[i]);
                Student newStudent = JsonConvert.DeserializeObject<Student>(serializedParent);
                var studentC = db.studentCourses.Where(x => x.StudentId == students[i].Id).OrderByDescending(c=>c.id).ToList();
                List<Course> AllCourses = db.courses.OrderByDescending(c => c.ID).ToList();
                
                var CoursesToAssign = AllCourses.Where(o => !studentC.Any(p=>p.CourseId==o.ID)).ToList();
                var CoursesToRemove = AllCourses.Where(o => studentC.Any(p => p.CourseId == o.ID)).ToList();

                 studentCourses.Add(new StudentCoursesVM()
                {
                    student = newStudent,
                    coursesToAssign = CoursesToAssign,
                    coursesToRemove= CoursesToRemove

                });
            }
            return studentCourses;
        }
        public bool regesterStudentInCourse(string studentId, int courseId)
        {
            try
            {
                db.studentCourses.Add(new StudentCourse()
                {
                    CourseId = courseId,
                    StudentId = studentId
                });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
          
        }
        public bool removeStudentFromCourse(string studentId, int courseId)
        {
            try
            {
                var deletedStendetCourse=db.studentCourses.FirstOrDefault(c => c.CourseId == courseId && c.StudentId== studentId );
                if (deletedStendetCourse != null)
                {
                    db.studentCourses.Remove(deletedStendetCourse);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch
            {
                return false;
            }
        }
    }
  
}
