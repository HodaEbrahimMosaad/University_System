﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class ApplicationUser: IdentityUser
    {

        public Role UserRole { set; get; }

        [DataType(DataType.Text)]
        public string Bio { set; get; }

        public Status status { set; get; }


        [Required(ErrorMessage = "FName name is required")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "FName should be minimum 3 characters and a maximum of 20 characters")]
        [DataType(DataType.Text)]
        public string Fname { get; set; }


        [Required(ErrorMessage = "LName name is required")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "LName should be minimum 3 characters and a maximum of 20 characters")]
        [DataType(DataType.Text)]
        public string Lname { get; set; }

        //[Column(TypeName = "nvarchar(200)")]
        [DisplayName("Image Name")]
        public string ImgName { set; get; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
    }
}
