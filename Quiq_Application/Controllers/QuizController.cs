using Microsoft.AspNetCore.Mvc;

namespace Quiq_Application.Controllers
{
    public class QuizController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();
        public IActionResult Quizes()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            List<Models.QuizModel> quiz = (from x in context.Quizzes
            join y in context.Courses on x.CourseId equals y.CourseId
                                           select new Models.QuizModel
            {
                QuizId = x.QuizId,
                NumberOfQuestions = x.NumberOfQuestions,
                CourseId = x.CourseId,
                CourseName = y.CourseName,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                RoomCode = x.RoomCode,
                Mark = x.Mark
            }).ToList();

            return View(quiz);

        }

        public IActionResult AddNewQuiz()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Courses = context.Courses.ToList();
            return View();
        }
        public IActionResult SaveExam(Models.QuizModel quiz)
        {
            Entity.Quiz quiz1 = new Entity.Quiz();
     
            quiz1.QuizId = quiz.QuizId;
            quiz1.NumberOfQuestions = quiz.NumberOfQuestions;
            quiz1.CourseId = quiz.CourseId;
            quiz1.Name = quiz.Name;
            quiz1.RoomCode = quiz.RoomCode;
            quiz1.Mark = quiz.Mark;


            quiz1.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

            context.Quizzes.Add(quiz1);
            context.SaveChanges();

            return RedirectToAction("Quizes");
        }

        public IActionResult DeleteQuiz(int QuizId)
        {
            Entity.StudentQuiz studentQuiz = context.StudentQuizzes.Where(y=>y.QuizeId == QuizId).FirstOrDefault();
            Entity.Quiz quiz = context.Quizzes.Where(x => x.QuizId == QuizId).FirstOrDefault();

            if (quiz!=null)
            {
                if(studentQuiz == null)
                {
                   context.Quizzes.Remove(quiz);
                   context.SaveChanges();
                    return RedirectToAction("Quizes");
                }
                else
                {
                  return RedirectToAction("QuizIsCompleated");
                }
            }
            else
            {
                return RedirectToAction("Quizes");
            }
        }

        public IActionResult QuizIsCompleated()
        {
            return View();
        }



    }
}
