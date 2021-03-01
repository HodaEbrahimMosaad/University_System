using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Data;
using MVCCore03Osama.ViewModel;


namespace MVCCore03Osama.Service
{
    public interface IInstructor
    {
        public Task<List<Course>> GetInstructorCourses(string InsId);
        public Task<List<Instructor>> GetAll();
        public Task<Instructor> GetDetails(string id);
        public Task<bool> DeleteInstructor(string id);
        public Task<Instructor> Edit(string id);
        public Task<bool> UpdateInstructor(string id, Instructor instructor);
        public Task<List<InstructorCourseVM>> getAllActiveInstructors();
        public bool regesterInstructorInCourse(string InsId, int courseId);
        public bool removeInstructorFromCourse(string InsId, int courseId);
    }
}
