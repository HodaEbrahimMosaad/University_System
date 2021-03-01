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

        public async Task<Instructor> Edit(string id)
        {
            if (id == null)
            {
                return null;
            }

            var instructors = await userManager.Users.Where
                    (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();
            var instructor = instructors.SingleOrDefault(t => t.Id == id);
            if (instructor == null)
            {
                return null;
            }
            var serializedParent = JsonConvert.SerializeObject(instructor);
            var ins = JsonConvert.DeserializeObject<Instructor>(serializedParent);
            return ins;
        }

        public async Task<bool> DeleteInstructor(string id)
        {
            try
            {
                var instructors = await userManager.Users.Where
                    (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();
                var instructor = instructors.SingleOrDefault(t => t.Id == id);
                if (instructor != null)
                {
                    instructor.status = Status.Blocked;
                    var AllCourses = await db.courses.ToListAsync();
                    var setNull = AllCourses.Where(c => c.InstructorId == instructor.Id).ToList();
                    setNull.ForEach(c => c.InstructorId = null);
                    var crsIDs = setNull.Select(c => c.ID).ToList();
                    var StuCour = await db.studentCourses.ToListAsync();
                    var stucrsToRemove = StuCour.Where(sc => crsIDs.Contains(sc.CourseId)).ToList();
                    db.RemoveRange(stucrsToRemove);
                    db.Users.Update(instructor);
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
        public async Task<bool> UpdateInstructor(string id, Instructor _instructor)
        {
            if (id != _instructor.Id)
            {
                return false;
            }
            try
            {
                var instructors = await userManager.Users.Where
                    (u => u.status == Status.Active && u.UserRole == Role.Instructor).ToListAsync();
                var instructor = instructors.SingleOrDefault(t => t.Id == id);
                if (instructor != null)
                {
                    instructor.Fname = _instructor.Fname;
                    instructor.Lname = _instructor.Lname;
                    instructor.Email = _instructor.Email;
                    instructor.UserName = _instructor.UserName;
                    instructor.PhoneNumber = _instructor.PhoneNumber;
                    instructor.Bio = _instructor.Bio;
                    db.Users.Update(instructor);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                db.Users.Update(_instructor);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
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
                    var ListToExeptStudntCourse = newInstructor.Courses;
                    var courseWithIns = db.courses.Where(c => c.InstructorId != null).ToList();
                    CoursesToAssign = ListToExeptStudntCourse.Where(o => !courseWithIns.Any(p => p.ID == o.ID)).ToList();
                   
                }
                InstructorCourses.Add(new InstructorCourseVM()
                {
                    instructor = newInstructor,
                    coursesToAssign = CoursesToAssign.ToList(),
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

        public async Task<List<Course>> GetInstructorCourses(string InsId)
        {
            var courses = db.courses.Where(c => c.InstructorId == InsId).ToList();

            return courses;
        }
    }
}
