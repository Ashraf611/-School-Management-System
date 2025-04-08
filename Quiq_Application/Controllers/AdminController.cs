using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Quiq_Application.Controllers
{
    public class AdminController : Controller
    {

        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();

        public IActionResult Index(int UserId)
        {
            

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();

            ViewBag.dminNumber = context.Admins.Count();
            ViewBag.TeacherNumber = context.Teachers.Count();
            ViewBag.StudentsNumber =context.Students.Where(x=>x.Validation==true).Count();
            ViewBag.CourseNumbers = context.Courses.Count();
            return View();
        }

        public IActionResult GetAllAdmins()
        {
            

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            List<Models.UserModel> obj = context.Users
                .Where(x => x.UserTypeId.Equals(1)&&x.UserValidation.Equals(true))
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


        public IActionResult AddNewAdmin()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.address = context.UserAddresses.ToList();

            return View();
        }

        public IActionResult SaveAdmin(Models.UserModel userModel)
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


            Entity.Admin admin = new Entity.Admin();
            admin.UserId = user.UserId;
            context.Admins.Add(admin);

            context.SaveChanges();





            return RedirectToAction("GetAllAdmins");
        }


        public IActionResult DeleteAdmin(int UserId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Find all Admin records associated with the User
                    var admins = context.Admins.Where(a => a.UserId == UserId).ToList();

                    if (admins.Any()) // Check if any Admin records exist
                    {
                        context.Admins.RemoveRange(admins);
                        context.SaveChanges(); // Save after removing Admin records
                    }

                    // Now find and delete the User
                    var user = context.Users.FirstOrDefault(x => x.UserId == UserId);

                    if (user != null)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges(); // Save after removing User
                    }

                    transaction.Commit(); // Commit transaction if all operations succeed
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback in case of an error
                    return BadRequest($"Error: {ex.Message}");
                }
            }

            return RedirectToAction("GetAllAdmins");
        }






        public IActionResult EditAdmin(int UserId)
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
            userModel.UserValidation = user.UserValidation;


            return View(userModel);
        }


        public IActionResult Update(Models.UserModel userModel)
        {

            Entity.User user = context.Users.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.UserAddressId = userModel.UserAddressId;
            userModel.Gender = userModel.Gender;
            userModel.UserTypeId = userModel.UserTypeId;
            userModel.UserValidation = userModel.UserValidation;
            Entity.Admin admin = context.Admins.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            admin.UserId = userModel.UserId;
            context.SaveChanges();
            Entity.Admin admin1 = context.Admins.Where(x => x.UserId == userModel.UserId).FirstOrDefault();
            admin1.UserId = userModel.UserId;
            context.SaveChanges();

            return RedirectToAction("GetAllAdmins");

        }




    }
}
