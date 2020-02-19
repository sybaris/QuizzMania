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
        
        /// <summary>
        /// Constructeur. Les paramètres seront injecté par l'IOC (injection de dépendance)
        /// </summary>
        /// <param name="quizzManiaContext"></param>
        public HomeController(QuizzManiaContext quizzManiaContext)
        {
            _quizzManiaContext = quizzManiaContext;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
                return View("Login");

            if (userModel.IsAdmin)
                return RedirectToAction("AnswersWhiteboard", "Home", userModel);

            return RedirectToAction("UserSurvey", "Home", userModel);
        }

        public IActionResult UserSurvey(UserViewModel userModel)
        {
            var q = from user in _quizzManiaContext.Users
                    where user.FirstName.ToLower() == userModel.FirstName.ToLower()
                    select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            if (q.Count() != 1)
                throw new Exception($"L'utilisateur {userModel.FirstName} n'a pas été trouvé");
            return View(q.Single());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public IActionResult AnswersWhiteboard(UserViewModel userModel)
        {
            var q = from user in _quizzManiaContext.Users
                    where !user.IsAdmin
                    orderby user.FirstName
                    select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            var players = q.ToList();
            return View(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
