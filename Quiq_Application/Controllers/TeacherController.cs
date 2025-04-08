using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Linq;
using Quiq_Application.Entity;

namespace Quiq_Application.Controllers
{
    public class TeacherController : Controller
    {


        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }


        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();
        public IActionResult GetAllTeachers()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            List<Models.UserModel> obj = context.Users
                .Where(x => x.UserTypeId.Equals(2)&&x.UserValidation == true)
                .Select(x => new Models.UserModel
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    UserAddressId = x.UserAddressId,
                    UserTypeId = x.UserTypeId,
                    UserValidation = x.UserValidation,
                    Password = x.Password,
                    Gender = x.Gender
                }).ToList();

            return View(obj);
        }





        public IActionResult AddNewTeacher()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.address = context.UserAddresses.ToList();

            return View();
        }

        public IActionResult SaveTeacher(Models.UserModel userModel)
        {

            Entity.User user = new Entity.User();
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.Gender = userModel.Gender;
            user.UserAddressId = userModel.UserAddressId;
            user.UserTypeId = userModel.UserTypeId;
            user.UserValidation = true;
            context.Users.Add(user);
            context.SaveChanges();


            Entity.Teacher teacher = new Entity.Teacher();
            teacher.UserId = user.UserId;
            context.Teachers.Add(teacher);

            context.SaveChanges();


            return RedirectToAction("GetAllTeachers");
        }




        public IActionResult DeleteTeacher(int UserId)
        {
            var teacher = context.Teachers.FirstOrDefault(x => x.UserId == UserId);
            var user = context.Users.FirstOrDefault(x => x.UserId == UserId);

            if (teacher == null || user == null)
            {
                return NotFound();
            }

            
            var courses = context.Courses.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            var courseIds = courses.Select(c => c.CourseId).ToList();

            var studentQuizzes = context.StudentQuizzes.Where(x => courseIds.Contains(x.CourseId)).ToList();
            var quizzes = context.Quizzes.Where(x => courseIds.Contains(x.CourseId)).ToList();
            var questions = context.Questions.Where(x => courseIds.Contains(x.CourseId)).ToList();
            var students = context.Students.Where(x => courseIds.Contains(x.CourseId)).ToList();

           
            if (studentQuizzes.Any()) context.StudentQuizzes.RemoveRange(studentQuizzes);
            if (quizzes.Any()) context.Quizzes.RemoveRange(quizzes);
            if (questions.Any()) context.Questions.RemoveRange(questions);
            if (students.Any()) context.Students.RemoveRange(students);
            if (courses.Any()) context.Courses.RemoveRange(courses);

            context.Teachers.Remove(teacher);
            context.SaveChanges();

            
            context.Users.Remove(user);
            context.SaveChanges(); 

            return RedirectToAction("GetAllTeachers");
        }









        public IActionResult EditTeacher(int UserId)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.address = context.UserAddresses.ToList();
            Entity.User user = context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            Models.UserModel userModel = new Models.UserModel();
            userModel.UserId = user.UserId;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.Email = user.Email;
            userModel.Password = user.Password;
            userModel.Gender = userModel.Gender;
            userModel.UserAddressId = user.UserAddressId;
            userModel.UserTypeId = user.UserTypeId;
            userModel.UserValidation = true;

            return View(userModel);
        }


        public IActionResult Update(Models.UserModel userModel)
        {

            Entity.User user = context.Users.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            Entity.Teacher teacher = context.Teachers.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (user != null && teacher != null)
            {
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.Gender = userModel.Gender;
            user.UserAddressId = userModel.UserAddressId;
            user.UserTypeId = userModel.UserTypeId;
            user.UserValidation = true;
            context.SaveChanges();
            
            teacher.UserId = userModel.UserId;
            context.SaveChanges();

                return RedirectToAction("GetAllTeachers");
            
            }
            else
            {
                return Content("Teacher not found");
            }
           
           
  

         
        }
        //-----------------------------------------------------------

        public IActionResult ProfileTeacher()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            Entity.User user = context.Users.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();

            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Gender = user.Gender;

            return View();


        }





    }
}
