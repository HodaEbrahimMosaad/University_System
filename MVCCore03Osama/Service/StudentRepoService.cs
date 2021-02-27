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

        public  async Task< List<Student>> getAllStudents()
        {
            var allStudensBeforConvert= await userManager.Users.Where(u=>u.UserRole==Role.Student).ToListAsync();
            var allStudensInJson = JsonConvert.SerializeObject(allStudensBeforConvert);
            List<Student> allStudents = JsonConvert.DeserializeObject<List<Student>>(allStudensInJson);
            return allStudents;
        }
        public  void EditStudent( Student student)
        {
            var std = userManager.Users.FirstOrDefault(u => u.Id == student.Id);
            std.Fname = student.Fname;
            std.PhoneNumber = student.PhoneNumber;
            std.Bio = student.Bio;
            std.Email = student.Email;
            std.Lname = student.Lname;
            std.status = student.status;
            std.UserName = student.UserName;
            //userManager.ChangePasswordAsync(std, "123@aA", student.PasswordHash);
            // userManager.UpdateAsync(student);
            // db.Entry(std).State = EntityState.Modified;
            db.Users.Update(std);
             db.SaveChanges(); 
            
        }
        public Student getStudent(string id)
        {
            var studen = userManager.Users.FirstOrDefault(u => u.UserRole == Role.Student && u.Id == id);
            var StudentInJson = JsonConvert.SerializeObject(studen);
            Student FinalStudent = JsonConvert.DeserializeObject<Student>(StudentInJson);
            return FinalStudent;
        }
        public bool deleteStudent(string id ,Student student)
        {
            var std = db.Users.FirstOrDefault(u => u.Id == id);
            if (std == null)
                return false;
            try
            {
                std.status = Status.Blocked;
                db.Users.Update(std);
                //to remove his courses
                var stdCrs =db.studentCourses.Where(c => c.StudentId == std.Id);
                stdCrs.ForEachAsync(c => c.StudentId = null);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task< bool> createStudent(Student student)
        {
            Student std = new Student()
            {
                UserName = student.Email,
                Email = student.Email,
                Fname = student.Fname,
                Lname = student.Lname,
                Bio = student.Bio,
                UserRole = Role.Student,
                status = Status.Active,
                ImgName= "def.jfif"
            };
            var result= await userManager.CreateAsync(std, student.PasswordHash);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(std, "Student");
                return true;
            }
            return false;

            
        }
    }
  
}
