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
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View(student.getStudent(id));
        }
        [HttpPost]
        public IActionResult Delete(string id ,Student _student)
        {
            bool isDeleted = student.deleteStudent(id, _student);
            if (isDeleted)
            {
                return RedirectToAction(nameof(allStudents));
            }
            return View();
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
                return RedirectToAction(nameof(allStudents));
            }
            return View( _student);
        }
    }

}
