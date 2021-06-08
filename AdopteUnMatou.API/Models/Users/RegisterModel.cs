using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Models.Users
{
    public class RegisterModel
    {
        /// <summary>
        /// The user's profile picture
        /// </summary>
        public byte[] ProfilePicture { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        /// <example>Victor</example>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// The user's last name
        /// </summary>
        /// <example>DENIS</example>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        /// <example>MySuperSecurePassword</example>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The user's role. The roles are : Admin, Adoptant
        /// </summary>
        /// <example>Adoptant</example>
        [Required]
        public string Role { get; set; }

    }
}
