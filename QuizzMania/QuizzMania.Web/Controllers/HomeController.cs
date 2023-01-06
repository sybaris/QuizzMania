using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizzMania.BusinessLogicLayer;
using QuizzMania.Web.Models;

namespace QuizzMania.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessLayer _businessLayer;
        /// <summary>
        /// Constructeur. Les paramètres seront injecté par l'IOC (injection de dépendance)
        /// </summary>
        public HomeController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
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
            if (!ModelState.IsValid || !_businessLayer.ExistUser(userModel.FirstName))
            {
                ViewData["ErrorDisplay"] = true;
                return View("Login");
            }
            var user = _businessLayer.GetUser(userModel.FirstName);
            if (user.IsAdmin != userModel.IsAdmin)
            {
                ViewData["ErrorDisplay"] = true;
                return View("Login");
            }

            if (userModel.IsAdmin)
                return RedirectToAction("AnswersWhiteboard", "Home", userModel);

            return RedirectToAction("UserSurvey", "Home", userModel);
        }

        /// <summary>
        /// Formulaire de réponse de l'utilisateur
        /// </summary>
        public IActionResult UserSurvey(UserViewModel userModel)
        {
            var user = _businessLayer.GetUser(userModel.FirstName);
            var userVm = new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            return View(userVm);
        }

        /// <summary>
        /// Tableau de l'admin qui voit qui a répondu ou non, et les réponses si il les affiche
        /// </summary>
        public IActionResult AnswersWhiteboard(UserViewModel userModel)
        {
            var players = _businessLayer.GetPlayers().Select(user => new UsersViewModel() { FirstName = user.FirstName, Id = user.Id });
            return View(players);
        }

        /// <summary>
        /// En cas d'erreur...
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
