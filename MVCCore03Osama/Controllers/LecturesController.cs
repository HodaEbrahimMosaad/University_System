using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;

namespace MVCCore03Osama.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lectures/Index/5
        public IActionResult Index(int courseId)
        {
            //var applicationDbContext = _context.lectures.Where(l => l.CourseId == courseId);
            ViewData["CourseId"] = courseId;
            ViewData["LectureId"] = Request.Query["LectureID"];
            
            return View();
        }

        //// GET: Lectures/Create
        //public IActionResult Create()
        //{
        //    ViewData["CourseId"] = new SelectList(_context.courses, "ID", "Describtion");
        //    ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id");
        //    return View();
        //}

        // POST: Lectures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("ID,Name,Date,InstructorId,CourseId")] 
        public async Task<IActionResult> Create(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                lecture.Date = DateTime.Now;
                _context.Add(lecture);
                await _context.SaveChangesAsync();
                //ViewData["CourseId"] = courseId;
                //ViewData["LectureId"] = Request.Query["LectureID"]
                
                //return RedirectToAction(nameof(Index), _params);
                //return RedirectToAction("Index", "HomeUser", _params);
            }
            //ViewData["CourseId"] = new SelectList(_context.courses, "ID", "Describtion", lecture.CourseId);
            //ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", lecture.InstructorId);
            //return View(lecture);
            var _params = new { CourseID = Request.Query["CourseId"] };
            return RedirectToAction("Index", "HomeUser", _params);
        }

        //// GET: Lectures/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lecture = await _context.lectures.FindAsync(id);
        //    if (lecture == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CourseId"] = new SelectList(_context.courses, "ID", "Describtion", lecture.CourseId);
        //    ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", lecture.InstructorId);
        //    return View(lecture);
        //}

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Date,InstructorId,CourseId")] Lecture lecture)
        {
            if (id != lecture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["CourseId"] = new SelectList(_context.courses, "ID", "Describtion", lecture.CourseId);
            //ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", lecture.InstructorId);
            //return View(lecture);
            var _params = new { CourseID = Request.Query["CourseId"] };
            return RedirectToAction("Index", "HomeUser", _params);
        }

        //// GET: Lectures/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lecture = await _context.lectures
        //        .Include(l => l.Course)
        //        .Include(l => l.Instructor)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (lecture == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lecture);
        //}

        //// POST: Lectures/Delete/5
        //[ActionName("Delete")]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var lecture = await _context.lectures.FindAsync(id);
            _context.lectures.Remove(lecture);
            await _context.SaveChangesAsync();

            var _params = new { CourseID = Request.Query["CourseId"] };
            return RedirectToAction("Index", "HomeUser", _params);
            //return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> removeLec(int id)
        {
            var lecture = await _context.lectures.FindAsync(id);
            _context.lectures.Remove(lecture);
            await _context.SaveChangesAsync();

            var _params = new { CourseID = Request.Query["CourseId"] };
            return RedirectToAction("Index", "HomeUser", _params);
            //return RedirectToAction(nameof(Index));
        }


        private bool LectureExists(int id)
        {
            return _context.lectures.Any(e => e.ID == id);
        }
    }
}
