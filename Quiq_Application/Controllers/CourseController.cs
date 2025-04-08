using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Quiq_Application.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;

namespace Quiq_Application.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();
        public IActionResult GetAllCourses()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            List<Models.CourseModel> obj = context.Courses
                .Select(x => new Models.CourseModel
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    Description = x.Description,
                    TeacherId = x.TeacherId,
                    CatigoryId = x.CatigoryId

                }).ToList();

            return View(obj);
        }



        public IActionResult AddNewCourse()
        {

            ViewBag.Username = HttpContext.Session.GetString("Username");

            ViewBag.Teacher = (from x in context.Teachers
                               join y in context.Users on x.UserId equals y.UserId
                               select new
                               {
                                   UserId = y.UserId,
                                   Name = y.FirstName + " " + y.LastName
                               }).ToList();



            ViewBag.Catigory = context.Categories.ToList();

            return View();
        }




        public IActionResult SaveCourse(Models.CourseModel courseModel)
        {

            Entity.Course course = new Entity.Course();
            course.CourseName = courseModel.CourseName;
            course.Description = courseModel.Description;
            course.TeacherId = context.Teachers
            .Where(x => x.UserId == courseModel.TeacherId)
            .Select(x => x.TeacherId)
            .FirstOrDefault();



            course.CatigoryId = courseModel.CatigoryId;


            context.Courses.Add(course);
            context.SaveChanges();



            return RedirectToAction("GetAllCourses");
        }




        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await context.Courses
                .Include(c => c.Questions)
                .Include(c => c.Quizzes)
                .ThenInclude(q => q.StudentQuizzes) 
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
            {
                return NotFound();
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
              
                if (course.Questions.Any())
                {
                    context.Questions.RemoveRange(course.Questions);
                }

               
                foreach (var quiz in course.Quizzes)
                {
                    if (quiz.StudentQuizzes.Any())
                    {
                        context.StudentQuizzes.RemoveRange(quiz.StudentQuizzes);
                    }
                }

                
                if (course.Quizzes.Any())
                {
                    context.Quizzes.RemoveRange(course.Quizzes);
                }

               context.Courses.Remove(course);

                if (course.Students.Any())
                {
                    context.Students.RemoveRange(course.Students);
                }

          
                

            
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction("GetAllCourses");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }







        public IActionResult EditCourse(int CourseId)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");

            ViewBag.Teacher = (from x in context.Teachers
                               join y in context.Users on x.UserId equals y.UserId
                               select new
                               {
                                   UserId = y.UserId,
                                   Name = y.FirstName + " " + y.LastName
                               }).ToList();

            ViewBag.Catigory = context.Categories.ToList();

            Entity.Course course = context.Courses.Where(x => x.CourseId == CourseId).FirstOrDefault();
            Models.CourseModel courseModel = new Models.CourseModel();
            courseModel.CourseId = course.CourseId;
            courseModel.CourseName = course.CourseName;
            courseModel.Description = course.Description;
            courseModel.TeacherId = course.TeacherId;
            courseModel.CatigoryId = course.CatigoryId;

            return View(courseModel);
        }

        public IActionResult Update(Models.CourseModel courseModel)
        {
            Entity.Course courseToEdit = context.Courses.Where(x => x.CourseId == courseModel.CourseId).FirstOrDefault();
            courseToEdit.CourseName = courseModel.CourseName;
            courseToEdit.Description = courseModel.Description;

            courseToEdit.TeacherId = context.Teachers
           .Where(x => x.UserId == courseModel.TeacherId)
           .Select(x => x.TeacherId)
           .FirstOrDefault();

            courseToEdit.CatigoryId = courseModel.CatigoryId;
            context.SaveChanges();

            return RedirectToAction("GetAllCourses");




        }
    }
}