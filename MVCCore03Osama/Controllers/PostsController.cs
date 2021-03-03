
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;


using MVCCore03Osama.Service;

using Microsoft.AspNetCore.Identity;

namespace MVCCore03Osama.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IPost _post { get; }
        public IComment _com { get; }
        public UserManager<ApplicationUser> UserManager_ { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }

        /*public InstructorController(IInstructor _instructor, ICourse _course, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)*/
        public PostsController(ApplicationDbContext context, IPost post_, IComment com_, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            this._post = post_;
            this._com = com_;
            this.UserManager_ = userManager;
            this._signInManager = signInManager;
        }

        // GET: Posts

        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.posts.Include(p => p.Lecture);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Posts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.posts
        //        .Include(p => p.Lecture)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //// GET: Posts/Create
        //public IActionResult Create()
        //{
        //    ViewData["LectureID"] = new SelectList(_context.lectures, "ID", "InstructorId");
        //    return View();
        //}

        //// POST: Posts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Title,Date,Body,ApplicationUserId,LectureID")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LectureID"] = new SelectList(_context.lectures, "ID", "InstructorId", post.LectureID);
        //    return View(post);
        //}

        //// GET: Posts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.posts.FindAsync(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LectureID"] = new SelectList(_context.lectures, "ID", "InstructorId", post.LectureID);
        //    return View(post);
        //}

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Date,Body,ApplicationUserId,LectureID")] Post post)
        //{
        //    if (id != post.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(post);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostExists(post.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LectureID"] = new SelectList(_context.lectures, "ID", "InstructorId", post.LectureID);
        //    return View(post);
        //}

        //// GET: Posts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.posts
        //        .Include(p => p.Lecture)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //// POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var post = await _context.posts.FindAsync(id);
        //    _context.posts.Remove(post);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PostExists(int id)
        //{
        //    return _context.posts.Any(e => e.ID == id);
        //}
        //public bool DeletePost(int ID)
        //{
        //    _post.DeletePost(ID);
        //    return true;
        //}
        //public bool Insert(string body , string Title, string ID)
        //{
        //    //return RedirectToAction("Index");
            
        //    //int lectureid = 3;

        //    Post p = new Post
        //    {
        //        Body = body,
        //        Title = Title,
        //        LectureID = 3,
        //        Date = DateTime.Now,
        //        ApplicationUserId = ID,
        //    };
        //    _post.Insert(p);
        //    /*_context.Add(p);
        //    _context.SaveChanges();*/
        //    return true;
        //}
        //public bool EditPost(int ID , string Body , string Title)
        //{
        //    _post.UpdatePost(ID, Body, Title);
        //    return true;
        //}
        ///////////=====================Aliaa===============================/////////
        ///
        [NoDirectAccess]
        [HttpGet]
        public async  Task< IActionResult> AddOrEdit(int id =0)
        {
            var post = new Post();
            if (id != 0)
            {
                post = await _context.posts.FirstOrDefaultAsync(z=>z.ID==id);
                return  View(post);
            }

            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int  id, Post post)
        {
            if (ModelState.IsValid)
            {
                var _post = _context.posts.FirstOrDefault(p => p.ID == id);
                if (_post == null)
                {
                   _context.posts.Add(post);
                    _context.SaveChanges();
                }
                else
                {
                    _post.Title = post.Title;
                    _post.Body = post.Body;
                    _post.Date = post.Date;
                    _context.SaveChanges();
                }
                var data = await _context.posts.Include(u => u.postOwner).OrderByDescending(z => z.Date)
                    .Include(p => p.Comments).ThenInclude(i => i.commentOwner).ToListAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView",
                 data.ToList()  ) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", post) });
        }

        public async Task<IActionResult> Index(int crsId )
        {
            
            var applicationDbContext = await _context.posts.Include(u => u.postOwner)
                .Include(p => p.Comments).ThenInclude(i=>i.commentOwner).OrderByDescending(z => z.Date).ToListAsync();
            return View(applicationDbContext);
        }

      
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Postt = await _context.posts.FirstOrDefaultAsync(p => p.ID == id);
            
            if (Postt != null)
            {
                _context.posts.Remove(Postt);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView",
                    await _context.posts.Include(u => u.postOwner).OrderByDescending(z => z.Date).Include(p => p.Comments)
                    .ThenInclude(i => i.commentOwner).ToListAsync()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Delete", Postt) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int postid, string commnetbody)
        {
            var user = await UserManager_.FindByNameAsync(User.Identity.Name);
            var _comment = new Comment()
            {
                Body = commnetbody,
                Date = DateTime.Now,
                PostID = postid,
                ApplicationUserId=user.Id
            };
            _context.comments.Add(_comment);
            _context.SaveChanges();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_allView", await _context.posts
                .Include(u => u.postOwner).Include(p => p.Comments)
                .ThenInclude(i => i.commentOwner).ToListAsync()) });
        }

    }
}
