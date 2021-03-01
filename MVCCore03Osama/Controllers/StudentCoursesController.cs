using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using MVCCore03Osama.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Controllers
{

    public class StudentCoursesController : Controller
    {
        private readonly ICourse course;
        private readonly ApplicationDbContext applicationDbContext;


        public StudentCoursesController(ApplicationDbContext _applicationDbContext,ICourse _course)
        {
            course = _course;
            applicationDbContext = _applicationDbContext;
        }

        public async Task<IActionResult> CourseDetails(int id)
        {
            Course c = await course.GetCourseDetails(id);
            var x = c;
            ViewBag.lectures = await course.GetCourselectures(id);
            return View(c);
        }

        public JsonResult GetLectureMaterial(int id)
        {
            var materials = applicationDbContext.materials.Where(m => m.LectureID == id).ToList();

            var o = JsonConvert.SerializeObject(materials);
            return Json(o);
        }

    }
}
