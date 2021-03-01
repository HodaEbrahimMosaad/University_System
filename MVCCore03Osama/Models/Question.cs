using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Models
{
    public class Question
    { //essay - choose - TF
        [Key]
        public int ID { get; set; }
        public string body { get; set; }
        public int mark { get; set; }
        public string ModelAnswer { get; set; }
        public QueType QueType { get; set; }
        public bool Active { get; set; }


        [ForeignKey("Course")]
        public int? CourseId { set; get; }

        public virtual Course Course { get; set; }


        public virtual ICollection<Choice> Choice { get; set; }
    }
}
