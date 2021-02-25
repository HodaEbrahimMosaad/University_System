using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Data;
using MVCCore03Osama.ViewModel;

namespace MVCCore03Osama.Service
{
    public interface IStudent
    {
        public  Task<List<StudentCoursesVM>> getAllActiveStudents();
        public bool regesterStudentInCourse(string studentId, int courseId);
        public bool removeStudentFromCourse(string studentId, int courseId);
    }
}
