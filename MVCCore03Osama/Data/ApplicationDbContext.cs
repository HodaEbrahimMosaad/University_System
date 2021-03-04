using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCCore03Osama.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       
        
        public DbSet<Course> courses { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Material> materials { get; set; }
        public DbSet<Post> posts { get; set; }
       
        public DbSet<StudentCourse> studentCourses { get; set; }

        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Choice> Choice { get; set; }

        public DbSet<AdminContact> AdminContact { get; set; }
    }
}
