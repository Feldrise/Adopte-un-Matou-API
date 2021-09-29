using AdopteUnMatou.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Contexts
{
    public interface IAdopteUnMatouContext
    {
        public IMongoCollection<User> User { get; }
    }
}
