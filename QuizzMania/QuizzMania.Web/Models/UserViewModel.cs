using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMania.Web.Models
{
    public class UserViewModel
    { 
        /// <summary>
        /// First name
        /// </summary>
        [Display (Name = "Prénom : ")]
        [Required (ErrorMessage = "Prénom requis ! ! !"),MinLength(3, ErrorMessage = " Il faut au moins 3 caractères ! ! !")]
        public string FirstName { get; set; }

        /// <summary>
        /// Is admin
        /// </summary>
        [Display(Name = "Admin : ")]
        public bool IsAdmin { get; set; }

    }
}

