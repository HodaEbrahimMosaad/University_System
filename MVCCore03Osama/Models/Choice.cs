using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Choice
    {
        [Key]
        public int ID { get; set; }
        public string ChoiceText { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; }

        public Question Question { get; set; }

    }
}
