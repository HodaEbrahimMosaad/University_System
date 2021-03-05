using Microsoft.EntityFrameworkCore;
using University.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class userCoursesService
    {
        //private readonly ICourseRepository TrackRepository;

        //public userCoursesService(ITrackRepository TrackRepository)
        //{
        //    this.TrackRepository = TrackRepository;
        //}

        private readonly ApplicationDbContext _context;

        public userCoursesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<Course>> getCourses() 
        {
            return _context.courses.Include(c=>c.Lectures).ToListAsync();
        }
    }
}
