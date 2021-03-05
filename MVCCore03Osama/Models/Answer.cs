using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }

        public string AnsText { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; }

        public Question Question { get; set; }


        [ForeignKey("Course")]
        public int? CourseId { set; get; }

        public virtual Course Course { get; set; }


        [ForeignKey("Student")]
        public string StudentId { set; get; }

        public virtual Student Std { get; set; }
    }
}
