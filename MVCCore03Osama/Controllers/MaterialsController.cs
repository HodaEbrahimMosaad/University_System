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
    public class MaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int crsid, int lecid, int id=0)
        {
            var material = new Material();
            ViewBag.crsID = crsid;
            ViewBag.lecID = lecid;
            
            if (id!=0)
                material = await _context.materials.FindAsync(id);

            return View(material);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,Path,Name,Description,LectureID")] Material material)
        {
            if (ModelState.IsValid) { 
            
                if (id == 0)
                {
                    _context.Add(material);
                    await _context.SaveChangesAsync();

                }
                else 
                {
                    try
                    {
                        _context.Update(material);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MaterialExists(material.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }
                //return Json(new { });
                return RedirectToAction("userhome", "Home");
                //return RedirectToAction("CourseDetails", "StudentCourses", new{ id= lecture.CourseId});
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCourses", _context.lectures.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", material) });
        }

        private bool MaterialExists(int id)
        {
            return _context.materials.Any(e => e.ID == id);
        }

        public bool Delete(int id)
        {
            var c = _context.materials.FirstOrDefault(c => c.ID == id);
            _context.materials.Remove(c);
            _context.SaveChanges();
            return true;
        }
    }
    
}
