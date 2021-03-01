using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Models
{
    public class StudentCourse
    {
        [Key]
        public int id { set; get; }
        [Required]
        public int Grade { set; get; }

        [ForeignKey("Course")]
        public int CourseId { set; get; }

        public virtual Course Crs { get; set; }


        [ForeignKey("Student")]
        public string StudentId { set; get; }

        public virtual Student Std { get; set; }

        public int Mark { get; set; }
        public int ExamStatus { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public int ApplicationUserId { set; get; }

        //public virtual ApplicationUser Std { get; set; }
    }
}
