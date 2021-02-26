using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using MVCCore03Osama.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Service
{
    public class InstructorRepoService:IInstructor
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public InstructorRepoService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> _userManager)
        {
            db = applicationDbContext;
            userManager = _userManager;
        }
        public async Task<Instructor> GetDetails(string id) {
            var instructors = await userManager.Users.Where
               (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();
            var instructor = instructors.SingleOrDefault(t => t.Id == id);
            var serializedParent = JsonConvert.SerializeObject(instructor);
            var ins = JsonConvert.DeserializeObject<Instructor>(serializedParent);
            return ins;
        }

        public async Task<List<Instructor>> GetAll()
        {
            var instructors = await userManager.Users.Where
               (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();

            List<Instructor> AllInstructors = new List<Instructor>();
            for (int i = 0; i < instructors?.Count(); i++)
            {
                var serializedParent = JsonConvert.SerializeObject(instructors[i]);
                AllInstructors.Add(JsonConvert.DeserializeObject<Instructor>(serializedParent));
            }
            return AllInstructors;
        }

        public async Task<List<InstructorCourseVM>> getAllActiveInstructors()
        {

            var instructors = await userManager.Users.Where
                (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();
            List<InstructorCourseVM> InstructorCourses = new List<InstructorCourseVM>();
            for (int i = 0; i < instructors?.Count(); i++)
            {
                var serializedParent = JsonConvert.SerializeObject(instructors[i]);
                Instructor newInstructor = JsonConvert.DeserializeObject<Instructor>(serializedParent);
                var CoursesToAssign = await db.courses.ToListAsync();
                
                
               var CoursesToRemove =  await db.courses?.Where(c => c.InstructorId == newInstructor.Id).ToListAsync();
                 if (CoursesToRemove != null)
                {
                    // CoursesToAssign = await db.courses.Except(CoursesToRemove).ToListAsync();
                    newInstructor.Courses =  db.courses.AsEnumerable().Except(CoursesToRemove).ToList(); 
                    //CoursesToAssign =  db.courses.Where(o => !CoursesToRemove.Any(p => p.ID == o.ID)).ToList();
                }
                InstructorCourses.Add(new InstructorCourseVM()
                {
                    instructor = newInstructor,
                    coursesToAssign = newInstructor.Courses.ToList(),
                    coursesToRemove=CoursesToRemove
                    
                });
            }
            return InstructorCourses;
        }
        public bool regesterInstructorInCourse(string InsId, int courseId)
        {
            try
            {
                var course =db.courses.FirstOrDefault(c => c.ID == courseId);
                course.InstructorId = InsId;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool removeInstructorFromCourse(string InsId, int courseId)
        {
            try
            {
                var Course = db.courses.FirstOrDefault(c=>c.InstructorId == InsId);
                if (Course != null)
                {
                    Course.InstructorId = null;
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
