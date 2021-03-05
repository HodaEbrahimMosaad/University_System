using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
namespace University.ViewModel
{
    public class InstructorCourseVM
    {
        public ApplicationUser instructor { set; get; }
        public List<Course> coursesToAssign { set; get; }
        public List<Course> coursesToRemove { set; get; }

    }
}
