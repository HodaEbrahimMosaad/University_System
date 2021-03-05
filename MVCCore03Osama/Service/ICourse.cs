using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Service
{
    public interface ICourse
    {
        public Task<List<Course>> GetAll();
        public Task<Course> GetCourseDetails(int id);
        public Task<List<Lecture>> GetCourselectures(int id);
    }
}
