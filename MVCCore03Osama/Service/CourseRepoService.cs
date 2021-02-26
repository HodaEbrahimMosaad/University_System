using Microsoft.EntityFrameworkCore;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Service
{
    public class CourseRepoService : ICourse
    {
        public CourseRepoService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<List<Course>> GetAll()
        {
            return await ApplicationDbContext.courses.ToListAsync();
        }
    }
}
