using Microsoft.AspNetCore.Mvc;
using Quiq_Application.Entity;
using Quiq_Application.Models;

namespace Quiq_Application.Controllers
{
    public class AcceptUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        Entity.QuizApplicationContext context = new Entity.QuizApplicationContext();

        public IActionResult AcceptUsers()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.acceptNumbers = context.Users.Where(x => x.UserValidation == false).Count();
            List<Models.UserModel> obj = (from x in context.Users
            .Where(x => x.UserValidation == false)
            join y in context.UserTypes on x.UserTypeId equals y.UserTypeId
            select new Models.UserModel
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserTypeName = y.UserTypeName,

            }).ToList();

            return View(obj);
        }



        public IActionResult Accept(int UserId)
        {
            var user = context.Users.Where(x => x.UserId == UserId).FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found.");
            }
            if(user.UserTypeId == 1)
            {
                Entity.Admin admin = new Entity.Admin();
                admin.UserId = UserId;
                context.Add(admin);
                user.UserValidation = true;
            }
            else if (user.UserTypeId == 2)
            {
                Entity.Teacher teacher = new Entity.Teacher();
                teacher.UserId = UserId;
                context.Teachers.Add(teacher);

                user.UserValidation = true;
            }
            else
            {
                Entity.Student studen = context.Students.Where(x=>x.UserId == UserId).FirstOrDefault();
                if (studen != null)
                {
                    studen.Validation = true;
                    user.UserValidation = true;
                }
                else
                {
                 return RedirectToAction("AcceptUsers");
                }
            }
            context.SaveChanges();

            return RedirectToAction("AcceptUsers");
        }




    }
}
