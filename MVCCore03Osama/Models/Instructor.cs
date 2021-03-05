using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Instructor : ApplicationUser
    {
        [DataType(DataType.Currency)]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Degree name is required")]
        [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Degree name should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Degree { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
