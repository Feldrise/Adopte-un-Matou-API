using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Models.Applications
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CatId { get; set; }

        public DateTime Date { get; set; }

        public string ApplicationStep { get; set; }

        // General informations
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AdultsNumber { get; set; }
        public string AdultsAge { get; set; }
        public string ChildrenNumber { get; set; }
        public string ChildrenAge { get; set; }

        public IList<ApplicationQuestion> Questions { get; set; }

        // Cat type
        public string CatAge { get; set; }
        public int CatSex { get; set; }
        public string CatNature { get; set; }
    }
}
