
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Material
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Path is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Path should be minimum 7 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string Path { get; set; }

        [Required(ErrorMessage = "Name of file is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name of file should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        [DisplayName("Name of material file")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(400, MinimumLength = 3,
        ErrorMessage = "Description should be minimum 3 characters and a maximum of 400 characters")]
        [DataType(DataType.Text)]
        public string Description { set; get; }
        [ForeignKey("Lecture")]
        public int LectureID { set; get; }

        public virtual Lecture Lecture { get; set; }

    }
}
