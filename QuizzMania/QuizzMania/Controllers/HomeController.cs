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
        public HomeController(QuizzManiaContext quizzManiaContext)
        {
            _quizzManiaContext = quizzManiaContext;
        }

        /// <summary>
        /// La page index renvoie sur la vue login
        /// </summary>
        public IActionResult Index()
        {
            return View("Login");
        }

        /// <summary>
        /// Page à propos
        /// </summary>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Login de l'utilisateur ou de l'admin
        /// </summary>
        public IActionResult Login(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
                return View("Login");

            if (userModel.IsAdmin)
                return RedirectToAction("AnswersWhiteboard", "Home", userModel);

            return RedirectToAction("UserSurvey", "Home", userModel);
        }

        /// <summary>
        /// Formulaire de réponse de l'utilisateur
        /// </summary>
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
        /// Tableau de l'admin qui voit qui a répondu ou non, et les réponses si il les affiche
        /// </summary>
        public IActionResult AnswersWhiteboard(UserViewModel userModel)
        {
            var q = from user in _quizzManiaContext.Users
                    where !user.IsAdmin
                    orderby user.FirstName
                    select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            var players = q.ToList();
            return View(players);
        }

        // TODO Karim, est ce utile ????
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
