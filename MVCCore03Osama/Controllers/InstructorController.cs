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
using MVCCore03Osama.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCCore03Osama.Controllers
{
    [Authorize]
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        public InstructorController(IInstructor _instructor, ICourse _course, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.Instructor_ = _instructor;
            this.Course = _course;
            this.UserManager_ = userManager;
            _signInManager = signInManager;
        }

        public IInstructor Instructor_ { get; }
        public ICourse Course { get; }
        public UserManager<ApplicationUser> UserManager_ { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(string id = null)
        {
            if (id == null)
            {
                //ViewBag.Courses = new SelectList(await Course.GetAll(), "Id", "Name");
                return View(new Instructor());
            }
            else
            {
                var Ins = await Instructor_.Edit(id);
                if (Ins == null)
                {
                    return NotFound();
                }
                return View(Ins);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Fname,Lname,PhoneNumber,Email,UserName,Bio")] Instructor Ins
            , string id = null, string password = null)
        {
            if (password != null)
            {
                try
                {
                    Ins.UserRole = Role.Instructor;
                    Ins.UserName = Ins.Email;
                    Ins.EmailConfirmed = true;
                    Ins.ImgName = "def.jfif";
                    var result = await UserManager_.CreateAsync(Ins, password);
                    if (!result.Succeeded)
                    {
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", Ins) });

                    }
                    //await _signInManager.SignInAsync(Ins, isPersistent: false);
                    await UserManager_.AddToRoleAsync(Ins, "Instructor");
                    ViewBag.FK_Course = await Course.GetAll();
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllInstructors", await Instructor_.GetAll()) });

                }
                catch
                {
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", Ins) });
                }
            }

            else
            {
                bool result = await Instructor_.UpdateInstructor(id, Ins);
                if (!result)
                {
                    
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", Ins) });

                }
                else{ 
                    ViewBag.FK_Course = await Course.GetAll();
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllInstructors", await Instructor_.GetAll()) });

                }
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", Ins) });
            }
        }

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
        
        public async Task<bool> Delete(string id)
        {
            bool result = await Instructor_.DeleteInstructor(id);
            return result;
        }

        public async Task<IActionResult> Edit(string id)
        {
            var Ins = await Instructor_.Edit(id);
            if (Ins == null)
            {
                return NotFound();
            }
            return View(Ins);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInstructor(string id, [Bind("Id,Fname,Lname,PhoneNumber,Email,UserName,Bio")] Instructor Ins)
        {
           
            bool result = await Instructor_.UpdateInstructor(id,Ins);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            } else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = new SelectList(await Course.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string password,ApplicationUser Ins)
        {
            Ins.UserRole = Role.Instructor;
            Ins.UserName = Ins.Email;
            Ins.EmailConfirmed = true;
            Ins.ImgName = "def.jfif";

            var result = await UserManager_.CreateAsync(Ins, password);
            //await _signInManager.SignInAsync(Ins, isPersistent: false);
            await UserManager_.AddToRoleAsync(Ins, "Instructor");
            return RedirectToAction(nameof(Index));
        }
    }
}
