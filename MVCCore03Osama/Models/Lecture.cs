using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace MVCCore03Osama.Models
{
    public class Lecture
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lecture name is required")]
        [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Lecture name should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [ForeignKey("Instructor")]
        [Required]
        public string InstructorId { set; get; }

        public virtual Instructor Instructor { set; get; }

        [ForeignKey("Course")]
        [Required]
        public int CourseId { set; get; }

        public virtual Course Course { set; get; }
    }
}
