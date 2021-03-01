using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Controllers
{
    public class ExamController : Controller
    {
        public ApplicationDbContext ApplicationDbContext { get; }

        public ExamController(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }
        public int CreateQuestion(string body, int mark,
            string modelAns , QueType QueType,  int CourseId)
        {
            bool Active = true;
            Question Q = new Question()
            {
                body = body,
                mark = mark,
                ModelAnswer = modelAns,
                QueType = QueType,
                CourseId = CourseId,
                Active = Active
            };
            ApplicationDbContext.Question.Add(Q);
            ApplicationDbContext.SaveChanges();

            return Q.ID;
            
        }

        public bool CreateChoice(string choice, int QueID)
        {
            Choice C = new Choice()
            {
                ChoiceText = choice,
                QuestionId = QueID
            };
            ApplicationDbContext.Choice.Add(C);
            ApplicationDbContext.SaveChanges();
            return true;
        }

        public bool 

        public IActionResult CreateExam()
        {

            return View();
        }
    }
}
