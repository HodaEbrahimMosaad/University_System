using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace MVCCore03Osama.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Created At")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [StringLength(300, MinimumLength = 3,
        ErrorMessage = "Comment should be minimum 3 characters and a maximum of 300 characters")]
        [DataType(DataType.Text)]
        public string Body { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostID { set; get; }
        public virtual Post Post { set; get; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { set; get; }

        public virtual ApplicationUser commentOwner { get; set; }
    }
}
