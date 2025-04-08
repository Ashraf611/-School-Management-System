using Microsoft.AspNetCore.Mvc;
using Quiq_Application.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace Quiq_Application.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();



        public IActionResult GetAllQuestions()
        {
            var iddd = HttpContext.Session.GetInt32("UserId");
            Entity.User userTemp = context.Users.Where(x => x.UserId == iddd).FirstOrDefault();
            ViewBag.UserType = userTemp.UserTypeId;

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            List<Models.QuestionModel> obj = (from x in context.Questions
             join y in context.Courses on x.CourseId equals y.CourseId
             select new Models.QuestionModel
            {
               QuestionsId = x.QuestionsId,
               Text = x.Text,
               FistOption = x.FistOption, // Corrected the typo
               SecontOption = x.SecontOption, // Corrected the typo
               ThirdOption = x.ThirdOption,
               FourthOption = x.FourthOption,
               Answer = x.Answer,
               CourseName = y.CourseName,
               Grade = x.Grade
            }).ToList();


            return View(obj);
        }



        public IActionResult AddNewQuestion()
        {
            var iddd = HttpContext.Session.GetInt32("UserId");
            Entity.User userTemp = context.Users.Where(x => x.UserId == iddd).FirstOrDefault();
            ViewBag.UserType = userTemp.UserTypeId;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Courses = context.Courses.ToList();

            return View();
        }

        public IActionResult SaveQuestion(Models.QuestionModel questionModel)
        {

            Entity.Question question = new Entity.Question();
            question.Text = questionModel.Text;
            question.FistOption = questionModel.FistOption;
            question.SecontOption = questionModel.SecontOption;
            question.ThirdOption = questionModel.ThirdOption;
            question.FourthOption = questionModel.FourthOption;
            question.Answer = questionModel.Answer;
            question.Grade = questionModel.Grade;
            question.CourseId = questionModel.CourseId;
            context.Questions.Add(question);
            context.SaveChanges();



            return RedirectToAction("GetAllQuestions");
        }


        public IActionResult DeleteQuestion(int QuestionsId)
        {
            Entity.Question question = context.Questions.Where(x => x.QuestionsId == QuestionsId).FirstOrDefault();
            context.Questions.Remove(question);
            context.SaveChanges();

            return RedirectToAction("GetAllQuestions");
        }



        public IActionResult EditQuestion(int QuestionsId)
        {
            var iddd = HttpContext.Session.GetInt32("UserId");
            Entity.User userTemp = context.Users.Where(x => x.UserId == iddd).FirstOrDefault();
            ViewBag.UserType = userTemp.UserTypeId;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.courses = context.Courses.ToList();
            Entity.Question question = context.Questions.Where(x => x.QuestionsId == QuestionsId).FirstOrDefault();
            Models.QuestionModel questionModel = new Models.QuestionModel();
            questionModel.Text = question.Text;
            questionModel.FistOption = question.FistOption;
            questionModel.SecontOption = question.SecontOption;
            questionModel.ThirdOption = question.ThirdOption;
            questionModel.FourthOption = question.FourthOption;
            questionModel.Answer = question.Answer;
            questionModel.Grade = question.Grade;
            questionModel.CourseId = question.CourseId;


            return View(questionModel);
        }


        public IActionResult Update(Models.QuestionModel questionModel)
        {
            Entity.Question question = context.Questions.Where(x => x.QuestionsId == questionModel.QuestionsId).FirstOrDefault();
            question.Text = questionModel.Text;
            question.FistOption = questionModel.FistOption;
            question.SecontOption = questionModel.SecontOption;
            question.ThirdOption = questionModel.ThirdOption;
            question.FourthOption = questionModel.FourthOption;
            question.Answer = questionModel.Answer;
            question.Grade = questionModel.Grade;
            question.CourseId = questionModel.CourseId;
            context.SaveChanges();


            return RedirectToAction("GetAllQuestions");

        }







    }
}
