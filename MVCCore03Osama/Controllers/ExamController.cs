using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MVCCore03Osama.Data;
using MVCCore03Osama.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public IActionResult ViewExam(int CourseId=2)
        {
            var url = Request.GetDisplayUrl();
            //var studentId = "5f63caa8-5409-4567-8205-d4087b6c41d4";
            //var Ans = ApplicationDbContext.Answer.FirstOrDefault
            //    (a => a.StudentId == studentId && a.CourseId == CourseId);
            //if (Ans != null)
            //{
            //    return BadRequest();
            //}
            var Choice = ApplicationDbContext.Choice.ToList();
            ViewBag.Choices = Choice;
            return View(ApplicationDbContext.Question.Where(q => q.CourseId == CourseId).ToList()) ;
        }


        public IActionResult ExamCorrection(int CourseId = 2)
        {
            var stdId = ApplicationDbContext.studentCourses.Where(sc => sc.CourseId == CourseId).
                Select(sc => sc.StudentId).ToList();
            var Students = ApplicationDbContext.Users.Where(s => stdId.Contains(s.Id)).ToList();
            return View(Students);
        }

        public JsonResult getStudentAnswer(string stuId, int crs=2)
        {
            List<Answer> Answer = new List<Answer>();
            Answer = ApplicationDbContext.Answer.Where(a => a.StudentId == stuId && a.CourseId == crs).ToList();
            //StudentCourse sc = ApplicationDbContext.studentCourses.FirstOrDefault(a => a.StudentId == stuId && a.CourseId == crs);
            var o = JsonConvert.SerializeObject(Answer);
            if (Answer.Count == 0)
            {
                dynamic que = new ExpandoObject();
                que.Empty = true;
                var oc = JsonConvert.SerializeObject(que);
                
                return Json(oc);
            }
            return Json(o);
        }


        public JsonResult CheckIfStudentTSubmitedExam(int CourseId, string StudentId)
        {
            dynamic flag = new ExpandoObject();


            var ans = ApplicationDbContext.Answer.FirstOrDefault
                (a => a.CourseId == CourseId && a.StudentId == StudentId);


            var queSum = ApplicationDbContext.Question.Where
                (a => a.CourseId == CourseId).Select(x => x.mark).Sum();


            if (ans == null)
            {
                flag.take = false;
                flag.marked = false;
            }else
            {
                flag.take = true;
                flag.queSum = queSum;
                var checkCorrection = ApplicationDbContext.studentCourses.
                FirstOrDefault(sc => sc.CourseId == CourseId && sc.StudentId == StudentId);
                if (checkCorrection.Mark > 0)
                {
                    flag.mark = checkCorrection.Mark;
                    flag.marked = true;
                }else
                {
                    flag.marked = false;
                }
            }

            var o = JsonConvert.SerializeObject(flag);
            return Json(o);
        }

        public bool SubmitAnswer(string AnsText,int QuestionId, int CourseId, string StudentId)
        {

            Answer Ans = new Answer()
            {
                AnsText = AnsText,
                QuestionId = QuestionId,
                CourseId = CourseId,
                StudentId = StudentId
            };
            ApplicationDbContext.Answer.Add(Ans);
            ApplicationDbContext.SaveChanges();
            return true;
        }

        public JsonResult GetQuestion(string AnsText, int id)
        {
            Question Question = ApplicationDbContext.Question.FirstOrDefault(a => a.ID == id);
            dynamic que = new ExpandoObject();
            que.ID = Question.ID;
            que.body = Question.body;
            que.mark = Question.mark;
            que.ModelAnswer = Question.ModelAnswer;
            que.QueType = Question.QueType;
            que.Active = Question.Active;
            que.AnsText = AnsText;

            var o = JsonConvert.SerializeObject(que); 
            return Json(o);
        }
        public bool UpdateMark(string stid, int crid, int mark)
        {
            var sc = ApplicationDbContext.studentCourses.FirstOrDefault(sc => sc.CourseId == crid && sc.StudentId == stid);
            sc.Mark = mark;
            ApplicationDbContext.studentCourses.Update(sc);
            ApplicationDbContext.SaveChanges();
            return true;
        }

        public JsonResult CheckIfCorrected(string stid, int crid)
        {
            dynamic que = new ExpandoObject();
            var sc = ApplicationDbContext.studentCourses.FirstOrDefault(sc => sc.CourseId == crid && sc.StudentId == stid);
            if(sc.Mark > 0)
            {
                que.mark = sc.Mark;
                que.corrected = true;
            };

            var o = JsonConvert.SerializeObject(que);
            return Json(o);
        }
    

        public IActionResult CreateExam()
        {

            return View();
        }
    }
}
