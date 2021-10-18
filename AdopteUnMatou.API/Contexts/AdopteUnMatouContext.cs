using AdopteUnMatou.API.Models.Cats;
using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Contexts
{
    public class AdopteUnMatouContext : IAdopteUnMatouContext
    {
        private readonly IMongoDatabase _database = null;

        public AdopteUnMatouContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.DatabaseName);
            }
        }

        public IMongoCollection<User> User { get { return _database.GetCollection<User>("users"); } }
        public IMongoCollection<Cat> Cat { get { return _database.GetCollection<Cat>("cats"); } }
    }
}
