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
            return View();
        }

        public IActionResult Login(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (userModel.IsAdmin)
            {
                return RedirectToAction("AnswersWhiteboard");
            }

            var a = from user in _quizzManiaContext.Users
                    where user.FirstName.ToLower() == userModel.FirstName.ToLower()
                    select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            if (a.Count() != 1)
                throw new Exception($"L'utilisateur {userModel.FirstName} n'a pas été trouvé");
            return View("UserSurvey", a.Single());
        }

        public IActionResult AnswersWhiteboard(UserViewModel userModel)
        {
            var q = from user in _quizzManiaContext.Users
                    where !user.IsAdmin
                    orderby user.FirstName
                    select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };

            var result = q.ToList();

            return View("AnswersWhiteboard", result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
