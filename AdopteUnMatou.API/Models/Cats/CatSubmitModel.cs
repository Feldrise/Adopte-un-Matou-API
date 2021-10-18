using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Models.Cats
{
    public class CatSubmitModel
    {
        public string Id { get; set; }

        /// <summary>
        /// The cat's image
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// The cat's adoption status. Can be Waiting, Reserved, Adopted
        /// </summary>
        /// <example>Waiting</example>
        public string AdoptionStatus { get; set; }

        /// <summary>
        /// The cat's name
        /// </summary>
        /// <example>Jack</example>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The cat's gender. Can be Male or Female
        /// </summary>
        /// <example>Male</example>
        [Required]
        public string Genre { get; set; }
        /// <summary>
        /// The cat's age
        /// </summary>
        /// <example>7 mois</example>
        [Required]
        public string Age { get; set; }

        /// <summary>
        /// The cat's price in €
        /// </summary>
        /// <example>179</example>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// The cat location
        /// </summary>
        /// <example>Paris</example>
        public string Location { get; set; }

        public IList<string> Properties { get; set; }

        /// <summary>
        /// The cat description
        /// </summary>
        /// <example>Jack est un super chat...</example>
        [Required]
        public string Description { get; set; }
    }
}
