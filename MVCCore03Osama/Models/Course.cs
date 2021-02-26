using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Total Grade is required")]
        [DisplayName("Total Grade")]
        public int TotalGrade { get; set; }

        [Required(ErrorMessage = "Describtion is required")]
        [StringLength(200, MinimumLength = 3,
        ErrorMessage = "Describtion should be minimum 3 characters and a maximum of 200 characters")]
        [DataType(DataType.Text)]
        public string Describtion { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Course name should be minimum 3 characters and a maximum of 5 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [ForeignKey("Instructor")]
        public string InstructorId { set; get; }

        public virtual Instructor instructor { get; set; }
        public virtual ICollection<StudentCourse> StdCrs { get; set; }
        public virtual ICollection<Lecture> Lectures { set; get; }
        
       

    }
}
