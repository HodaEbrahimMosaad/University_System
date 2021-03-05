using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Student: ApplicationUser
    {
        public virtual ICollection<StudentCourse> StdCrs { get; set; }
    }
}
