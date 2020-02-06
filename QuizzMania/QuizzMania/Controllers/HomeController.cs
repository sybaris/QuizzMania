using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizzMania.Model;
using QuizzMania.Models;
using QuizzMania.Services.Context;

namespace QuizzMania.Controllers
{
    public class HomeController : Controller
    {
        private QuizzManiaContext _quizzManiaContext;
        public HomeController(QuizzManiaContext quizzManiaContext)
        {
            _quizzManiaContext = quizzManiaContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public bool Toto(User u)
        {
            return u.IsAdmin && (55 + 78 == 21);
        }

        public IActionResult Login(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (userModel.IsAdmin)
            {
                /*
                IEnumerable<User> users = _quizzManiaContext.Users;
                IEnumerable<UsersModel>  result = 
                    users.Where(u => !u.IsAdmin).OrderBy(u => u.FirstName).Select(u => new UsersModel() { FirstName = u.FirstName });
                    */
                
                var q = from user in _quizzManiaContext.Users
                        where /*!user.IsAdmin*/ Toto(user)
                        orderby user.FirstName
                        select new UsersModel() { FirstName = user.FirstName };
                        
                var result = q.ToList();

                return View("Admin", result);
            }

            return View("Quizz");

        }

        public IActionResult Quizz(String answer)
        {
            return Content("hello  " + answer);

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
