using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using MVCCore03Osama.Data;
using System.Linq;

namespace MVCCore03Osama.ViewComponents
{
    public class ExamCorrectionViewComponent: ViewComponent
    {
        public ExamCorrectionViewComponent(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var url = Request.GetDisplayUrl();
            var CourseId = int.Parse(url.Split("/")[^1]);
            ViewBag.crs = CourseId;

            var stdId = ApplicationDbContext.studentCourses.Where(sc => sc.CourseId == CourseId).
                Select(sc => sc.StudentId).ToList();

            var Students = ApplicationDbContext.Users.Where(s => stdId.Contains(s.Id)).ToList();
            return View(Students);

        }
    }
}
