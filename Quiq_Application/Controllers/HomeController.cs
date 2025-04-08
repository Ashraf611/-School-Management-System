using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using Quiq_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace Quiq_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //---------------------login---------------------
        public IActionResult Login()
        {
            return View();
        }
        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();
        public IActionResult LoginTest(Models.UserModel userModel)
        {
            
            Entity.User user = context.Users.Where(x=>x.Password == userModel.Password && x.Email == userModel.Email).FirstOrDefault();
            if(user == null)
            {
                return View("Login");

            }
            else
            {
                if(user.UserTypeId.Equals(1) && user.UserValidation == true)
                {
                    HttpContext.Session.SetString("Username",user.FirstName+" "+user.LastName);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index","Admin");
                }
                else if(user.UserTypeId.Equals(2) && user.UserValidation == true)
                {
                    HttpContext.Session.SetString("Username", user.FirstName + " " + user.LastName);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index", "Teacher");
                }
                else if(user.UserTypeId.Equals(3) && user.UserValidation == true)
                {
                    HttpContext.Session.SetString("Username", user.FirstName + " " + user.LastName);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    return View("Login");
                }
                
            }
            
        }
        //---------------------Create Account---------------------
        public IActionResult Registration()
        {
            ViewBag.addresses = context.UserAddresses.ToList();
            ViewBag.userTypes = context.UserTypes.ToList();
            ViewBag.courses = context.Courses.ToList();
            return View();
        }
        public IActionResult MakeAnAccount(Models.RegestrationModel userModel)
        {
      
            Entity.User user = new Entity.User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Password = userModel.Password,
                UserAddressId = userModel.UserAddressId,
                UserTypeId = userModel.UserTypeId,
                UserValidation = false
            };
            context.Users.Add(user);
            context.SaveChanges(); 

            if (userModel.UserTypeId == 3) 
            {
          
                Entity.Student student = new Entity.Student
                {
                    UserId = user.UserId,  
                    CourseId = (int)userModel.CourseId,
                    Validation = false
                };
                context.Students.Add(student);
            }

            
            context.SaveChanges();

            return RedirectToAction("Login");
        }


        //---------------------------------------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
