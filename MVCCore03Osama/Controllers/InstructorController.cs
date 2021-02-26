using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Newtonsoft.Json;

namespace MVCCore03Osama.Controllers
{
    public class InstructorController : Controller
    {
        public InstructorController(IInstructor _instructor, ICourse _course)
        {
            this.Instructor_ = _instructor;
            this.Course = _course;
        }

        public IInstructor Instructor_ { get; }
        public ICourse Course { get; }

        public async Task<IActionResult> Index()
        {
            
            ViewBag.FK_Course = await Course.GetAll();
            return View(await Instructor_.GetAll());
        }

        public async Task<JsonResult> Detail(string id)
        {
            Instructor Ins = await Instructor_.GetDetails(id);

            //var test = new JavaScriptSerializer().Serialize(output);
            var o = JsonConvert.SerializeObject(Ins);
            //string json2 = JsonConvert.SerializeObject(o, Formatting.Indented);

            return Json(o);
        }

      
    }
}
