using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Data;
using University.ViewModel;


namespace University.Service
{
    public interface IInstructor
    {
        public Task<List<Course>> GetInstructorCourses(string InsId);
        public Task<List<ApplicationUser>> GetAll();
        public Task<Instructor> GetDetails(string id);
        public Task<bool> DeleteInstructor(string id);
        public Task<Instructor> Edit(string id);
        public Task<bool> UpdateInstructor(string id, Instructor instructor);
        public Task<List<InstructorCourseVM>> getAllActiveInstructors();
        public bool regesterInstructorInCourse(string InsId, int courseId);
        public bool removeInstructorFromCourse(string InsId, int courseId);
    }
}
