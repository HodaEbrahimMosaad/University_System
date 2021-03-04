using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;

namespace MVCCore03Osama.Controllers
{
    [Authorize]
    [Authorize(Roles = "Instructor")]
    public class LecturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int crsid, int id=0)
        {
            var lecture = new Lecture();
            ViewBag.crsID = crsid;
            
            if (id!=0)
                lecture = await _context.lectures.FindAsync(id);

            return View(lecture);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,Name,Date,InstructorId,CourseId")] Lecture lecture)
        {
            if (ModelState.IsValid) { 
            
                if (id == 0)
                {
                    _context.Add(lecture);
                    await _context.SaveChangesAsync();

                }
                else 
                {
                    try
                    {
                        _context.Update(lecture);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LectureExists(lecture.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }
                return Json(new { });
                //return RedirectToAction("CourseDetails", "StudentCourses", new{ id= lecture.CourseId});
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCourses", _context.lectures.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", lecture) });
        }

        private bool LectureExists(int id)
        {
            return _context.lectures.Any(e => e.ID == id);
        }

        public bool Delete(int id)
        {
            var c = _context.lectures.FirstOrDefault(c => c.ID == id);
            _context.lectures.Remove(c);
            _context.SaveChanges();
            return true;
        }
    }
    
}
