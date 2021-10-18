using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Models.Cats
{
    public class Cat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The cat's image id
        /// </summary>
        /// <example>6167e8ffba92f2bbb61f050c</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string ImageId { get; set; }

        /// <summary>
        /// The cat's adoption status. Can be Waiting, Reserved, Adopted
        /// </summary>
        /// <example>Waiting</example>
        public string AdoptionStatus { get; set; }

        /// <summary>
        /// The cat's name
        /// </summary>
        /// <example>Jack</example>
        public string Name { get; set; }
        /// <summary>
        /// The cat's gender. Can be Male or Female
        /// </summary>
        /// <example>Male</example>
        public string Genre { get; set; }
        /// <summary>
        /// The cat's age
        /// </summary>
        /// <example>7 mois</example>
        public string Age { get; set; }

        /// <summary>
        /// The cat's price in €
        /// </summary>
        /// <example>179</example>
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
        public string Description { get; set; }

    }
}
