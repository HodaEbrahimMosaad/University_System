using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MVCCore03Osama.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Title should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string Title { get; set; }


        [DisplayName("Created At")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Post body is required")]
        [StringLength(300, MinimumLength = 3,
        ErrorMessage = "Post body should be minimum 3 characters and a maximum of 300 characters")]
        [DataType(DataType.Text)]
        public string Body { get; set; }

        public virtual ICollection<Comment> Comments { set; get; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { set; get; }

        public virtual ApplicationUser postOwner { get; set; }
        [ForeignKey("Lecture")]
        public int LectureID { set; get; }

        public virtual Lecture Lecture { get; set; }
    }
}
