using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace University.Service
{
    public class CourseRepoService : ICourse
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseRepoService(ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        {
            ApplicationDbContext = applicationDbContext;
            signInManager = _signInManager;
            userManager = _userManager;
        }
        
        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<List<Course>> GetAll()
        {
            return await ApplicationDbContext.courses.ToListAsync();
        }
        public async Task<Course> GetCourseDetails(int id)
        {
            var course = await ApplicationDbContext.courses.SingleOrDefaultAsync(c => c.ID == id);

            return course;
            //return ApplicationDbContext.courses.Where(c => c.ID == id && c. userID);
        }

        public async Task<List<Lecture>> GetCourselectures(int id)
        {
            var lectures = await ApplicationDbContext.lectures.Where(l => l.CourseId == id).ToListAsync();

            return lectures;
        }

        //public async Task<JsonResult> GetLectureMaterial(int id)
        //{
        //    var materials = await ApplicationDbContext.materials.Where(m=>m.LectureID==id).ToListAsync();

        //    var o = JsonConvert.SerializeObject(materials);
        //    return JsonResult(o);
        //}

    }
}
