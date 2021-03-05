using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            return View(await _context.courses.ToListAsync());
        }

        //
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id=0)
        {
            ViewBag.ins = new SelectList(_context.Users.Where(u => u.UserRole == Role.Instructor).ToList(), "Id", "Fname");
            var course = new Course();
            if(id!=0)
                course = await _context.courses.FindAsync(id);

            return View(course);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,TotalGrade,Describtion,Name")] Course course)
        {
            if (ModelState.IsValid) { 
            
                if (id == 0)
                {
                    _context.Add(course);
                    await _context.SaveChangesAsync();

                }

                else 
                {
                    try
                    {
                        _context.Update(course);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CourseExists(course.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    //return RedirectToAction(nameof(Index));
                    
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCourses", _context.courses.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", course) });
        }

        

        private bool CourseExists(int id)
        {
            return _context.courses.Any(e => e.ID == id);
        }

        public bool Delete(int id)
        {
            var c =_context.courses.FirstOrDefault(c => c.ID == id);
            _context.courses.Remove(c);
            _context.SaveChanges();
            return true;
        }
        public IActionResult Preview()
        {
            ViewBag.flag = true;
            return RedirectToAction("Index");
        }

    }



    
}
