using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Models;
namespace MVCCore03Osama.ViewModel
{
    public class StudentCoursesVM
    {
        public Student student { set; get; }
        public List <Course> coursesToAssign { set; get; }
        public List<Course> coursesToRemove { set; get; }

    }
}
