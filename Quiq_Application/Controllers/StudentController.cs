using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Quiq_Application.Entity;
using Quiq_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Quiq_Application.Controllers
{
    public class StudentController : Controller
    {


        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }


        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();
        public IActionResult GetAllStudents()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            //List<Models.UserModel> obj = context.Users
            //    .Where(x => x.UserTypeId.Equals(3))
            //    .Select(x => new Models.UserModel
            //    {
            //        UserId = x.UserId,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        Email = x.Email,
            //        UserAddressId = x.UserAddressId,
            //        UserTypeId = x.UserTypeId,
            //        UserValidation = x.UserValidation,
            //        Password = x.Password,
            //        Gender = x.Gender
            //    }).ToList();

            List<Models.User_Student_CourseModel> obj = new List<Models.User_Student_CourseModel>();
             obj = (from user in context.Users.ToList()
                       join student in context.Students.ToList() on user.UserId equals student.UserId
                       join course in context.Courses.ToList() on student.CourseId equals course.CourseId   
                       where user.UserTypeId.Equals(3) && user.UserValidation == true
                       select new Models.User_Student_CourseModel{
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Gender = user.Gender,
                            UserAddressId = user.UserAddressId,
                            UserTypeId = user.UserTypeId,
                            UserValidation = user.UserValidation,
                            Password = user.Password,
                            CourseId = student.CourseId,
                           StudentId = student.StudentId,
                           CourseName = course.CourseName
                       }).ToList();


            return View(obj);
        }





        public IActionResult AddNewStudent()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.courses = context.Courses.ToList();
            ViewBag.address = context.UserAddresses.ToList();

            return View();
        }

        public IActionResult SaveStudent(Models.User_StudentModel user_StudentModel)
        {

            Entity.User user = new Entity.User
            {
                FirstName = user_StudentModel.FirstName,
                LastName = user_StudentModel.LastName,
                Email = user_StudentModel.Email,
                Password = user_StudentModel.Password,
                Gender = user_StudentModel.Gender,
                UserAddressId = user_StudentModel.UserAddressId,
                UserTypeId = user_StudentModel.UserTypeId,
                UserValidation = true
            };

            context.Users.Add(user);
            context.SaveChanges();  


            Entity.Student student = new Entity.Student
            {
                UserId = user.UserId,  
                CourseId = user_StudentModel.CourseId,
                Validation = true
            };

            context.Students.Add(student);
            context.SaveChanges();  

         
            return RedirectToAction("GetAllStudents");
        }




        public IActionResult DeleteStudent(int userId)
        {
            // Retrieve the student with related entities
            var student = context.Students
                .Include(s => s.StudentQuizzes) // Include related quizzes (or any dependent entities)
                .FirstOrDefault(s => s.UserId == userId);

            if (student != null)
            {
                // Delete dependent records first (if needed)
                context.StudentQuizzes.RemoveRange(student.StudentQuizzes);
                context.Students.Remove(student);
            }

            // Now, delete the user after removing the student
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                context.Users.Remove(user);
            }

            context.SaveChanges(); // Save changes after deletions
            return RedirectToAction("GetAllStudents");
        }





        public IActionResult EditStudent(int UserId)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.courses = context.Courses.ToList();
            ViewBag.address = context.UserAddresses.ToList();
            Entity.User user = context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            Models.User_StudentModel userModel = new Models.User_StudentModel();
            userModel.UserId = user.UserId;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.Email = user.Email;
            userModel.Password = user.Password;
            userModel.Gender = userModel.Gender;
            userModel.UserAddressId = user.UserAddressId;
            userModel.UserTypeId = user.UserTypeId;
            userModel.UserValidation = true;
            userModel.CourseId = context.Students.Where(x => x.UserId == UserId).FirstOrDefault().CourseId;

            return View(userModel);
        }


        public IActionResult Update(Models.User_StudentModel userModel)
        {
            Entity.User user = context.Users.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.UserAddressId = userModel.UserAddressId;
            user.Gender = userModel.Gender;
            user.UserTypeId = userModel.UserTypeId;
            user.UserValidation = true;

            Entity.Student student = context.Students.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            student.UserId = userModel.UserId;
            student.CourseId = userModel.UserId;
            
            context.SaveChanges();

            return RedirectToAction("GetAllStudents");

        }


        public IActionResult StudentProfile(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");

            return View();
        }

        public IActionResult Empty()
        {
            return View();
        }
        public IActionResult WrongEnter()
        {
            return View();
        }




        public IActionResult StudentQuiz()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");

            var student = context.Students.Where(x => x.UserId == userId).FirstOrDefault();
            var course = context.Courses.Where(x => x.CourseId.Equals(student.CourseId)).FirstOrDefault();
            var quiz = context.Quizzes.Where(x => x.CourseId == course.CourseId).ToList();
            foreach (Entity.Quiz item in quiz)
            {
               if (item == null)
               {
                return RedirectToAction("Empty"); 
               }
                return View();
            }
            

            return View();
        }




        public IActionResult StudentGetQuiz(Models.QuizModel quizModel)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");
            var student = context.Students.Where(x => x.UserId == userId).FirstOrDefault();
            var course = context.Courses.Where(x => x.CourseId == student.CourseId).FirstOrDefault();

            
            var quizzes = context.Quizzes.Where(x => x.CourseId == course.CourseId).ToList();

            
            var quiz = quizzes.FirstOrDefault(x => x.RoomCode == quizModel.RoomCode);

            if (quiz != null)
            {
              
                var studentQuiz = context.StudentQuizzes
                                        .FirstOrDefault(x => x.StudentId == student.StudentId && x.QuizeId == quiz.QuizId);

                if (studentQuiz != null && studentQuiz.Completed == true)
                {
                    
                    return RedirectToAction("WrongEnter");
                }

                Random random = new Random();
                int randomNumber = random.Next(1, 101);

              
                List<Models.QuestionModel> questionModels = context.Questions
                    .Where(q => q.CourseId == course.CourseId)
                    .Select(q => new Models.QuestionModel
                    {
                        QuestionsId = q.QuestionsId,
                        Text = q.Text,
                        FistOption = q.FistOption,
                        SecontOption = q.SecontOption,
                        ThirdOption = q.ThirdOption,
                        FourthOption = q.FourthOption,
                        Answer = q.Answer,
                        Grade = q.Grade,
                        CourseId = q.CourseId,
                        Actual = q.QuestionsId + randomNumber
                    }).ToList();

                return View(questionModels);
            }
            else
            {
                
                return RedirectToAction("WrongEnter");
            }
        }




        public IActionResult GoToExam(List<Models.QuestionModel> Questions)
        {
            int sum = 0;
            int TotalMarks = 0;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            
            var userId = HttpContext.Session.GetInt32("UserId");
            var student = context.Students.Where(x => x.UserId == userId).FirstOrDefault();
            var course = context.Courses.Where(x => x.CourseId == student.CourseId).FirstOrDefault();
            Entity.Quiz quiz = context.Quizzes.Where(x => x.CourseId == course.CourseId).FirstOrDefault();

            foreach (QuestionModel item in Questions)
            {
                if(item.Actual == item.Answer)
                {
                    sum += item.Grade;

                }
                if(item.QuestionsId != null)
                {
                    TotalMarks += item.Grade;
                }
            }

            Entity.StudentQuiz studentQuiz = new StudentQuiz();
            studentQuiz.StudentId = student.StudentId;
            studentQuiz.Score = sum;
            if(sum > TotalMarks / 2 && sum <= TotalMarks)
            {
                studentQuiz.Status = true;
            }
            else
            {
                studentQuiz.Status = false;
            }
            studentQuiz.Completed = true;
            studentQuiz.QuizeId = quiz.QuizId;
            studentQuiz.CourseId = course.CourseId;
            studentQuiz.AttemptDate = DateOnly.FromDateTime(DateTime.Now);
            context.StudentQuizzes.Add(studentQuiz);
            context.SaveChanges();


            return RedirectToAction("Index");
        }


        public IActionResult StudentResult()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");
            var student = context.Students.Where(x => x.UserId == userId).FirstOrDefault();
            var course = context.Courses.Where(x => x.CourseId == student.CourseId).FirstOrDefault();
           Entity.StudentQuiz studentQuiz = context.StudentQuizzes.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            if (studentQuiz != null)
            { 
                ViewBag.courseOfExam = course.CourseName;
                ViewBag.Score = studentQuiz.Score;
                if(studentQuiz.Status == true)
                {
                    ViewBag.Status = "Pass";
                }
                else
                {
                    ViewBag.Status = "Fail";
                }
                ViewBag.AttemptDate = studentQuiz.AttemptDate;

                return View();
            }
            else
            {
                return RedirectToAction("Empty");
            }
        }


        public IActionResult AllStudentResult()
        {
            var iddd = HttpContext.Session.GetInt32("UserId");
            Entity.User userTemp = context.Users.Where(x => x.UserId == iddd).FirstOrDefault();
            ViewBag.UserType = userTemp.UserTypeId;

            List<Models.StudentsResults_Model> studentQuizzes = (
            from studentQ in context.StudentQuizzes
            join y in context.Courses on studentQ.CourseId equals y.CourseId
            join z in context.Students on studentQ.StudentId equals z.StudentId
            join w in context.Users on z.UserId equals w.UserId
             select new Models.StudentsResults_Model
             {
            StudentName = w.FirstName + " " + w.LastName,
            CourseName = y.CourseName,
            Score = studentQ.Score,
            AttemptDate = studentQ.AttemptDate,
             Completed = studentQ.Completed
             }
             ).ToList();



            return View(studentQuizzes);
        }




}
}
