using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Models.Users
{
    public class UserSubmitModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The user's new profile picture
        /// </summary>
        public byte[] ProfilePicture { get; set; } 

        /// <summary>
        /// The user's first name
        /// </summary>
        /// <example>Victor</example>
        public string FirstName { get; set; }
        /// <summary>
        /// The user's last name
        /// </summary>
        /// <example>DENIS</example>
        public string LastName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        public string Email { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// The user's role. The roles are : Admin, Adoptant
        /// </summary>
        /// <example>Adoptant</example>
        public string Role { get; set; }
    }
}
