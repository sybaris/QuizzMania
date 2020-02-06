using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMania.Models
{
    public class UserModel
    { 
        /// <summary>
        /// First name
        /// </summary>
        [Required (ErrorMessage = "Prénom requis ! ! !")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Is admin
        /// </summary>
        public bool IsAdmin { get; set; }

    }
}

