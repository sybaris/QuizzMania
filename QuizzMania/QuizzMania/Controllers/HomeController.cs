using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizzMania.Model;
using QuizzMania.Models;
using QuizzMania.Services.Context;
using QuizzMania.Services;

namespace QuizzMania.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        /// <summary>
        /// Constructeur. Les paramètres seront injecté par l'IOC (injection de dépendance)
        /// </summary>
        public HomeController(IRepository repository)
        {
            _repository = repository;
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
            var user = _repository.GetUser(userModel.FirstName);
            var userVm = new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            return View(userVm);
        }

        /// <summary>
        /// Tableau de l'admin qui voit qui a répondu ou non, et les réponses si il les affiche
        /// </summary>
        public IActionResult AnswersWhiteboard(UserViewModel userModel)
        {
            var players = _repository.GetPlayers().Select(user => new UsersViewModel() { FirstName = user.FirstName, Id = user.Id });
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
