using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Service;
using MVCCore03Osama.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCCore03Osama.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private readonly IStudent student;
        public StudentsController(IStudent _student )
        {
            student = _student;
        }
        public async Task< IActionResult> allStudents()
        {
            return View(await student.getAllStudents());
        }

        [NoDirectAccess]
        [HttpGet]
        public IActionResult AddOrEdit(string  id="")
        {
            var std = new Student();
            if (id != "") 
            {
                std = student.getStudent(id);
            }
            
            return View(std);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(string id, Student _student)
        {
            if (ModelState.IsValid)
            {
                var std = student.getStudent(id);
                if (std == null)
                {
                   await student.createStudent(_student);
                }
                else
                {
                   student.EditStudent(_student);
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView",await student.getAllStudents()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", _student) });
        }


        [HttpGet]
        public  IActionResult Edit(string id)
        {
            var _student = student.getStudent(id);
            if (_student!=null)
            {
                return View(_student);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(  Student _student)
        {
            if (ModelState.IsValid)
            {
                student.EditStudent(_student);
                return RedirectToAction("allStudents");
            }
            return View(_student);
        }
        public IActionResult Details(string id)
        {

                return View(student.getStudent(id));
            
        }
        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    return View(student.getStudent(id));
        //}
        //[HttpPost]
        //public async Task<IActionResult>Delete(string id )
        //{
        //    var _student = student.getStudent(id);
        //    bool isDeleted = student.deleteStudent(id, _student);
        //    if (isDeleted)
        //    {
        //        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView", await student.getAllStudents()) });
        //    }

        //    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Delete", _student) });
        //}
        public bool Delete(string id)
        {
            var _student = student.getStudent(id);
            student.deleteStudent(id, _student);
            
            return true;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( Student _student)
        {
           bool isCreated = await student.createStudent( _student);
            if (isCreated == true)
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView", await student.getAllStudents()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", _student) });
        }
    }

}
