using AdopteUnMatou.API.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.DataProvider.Interfaces
{
    public interface IDPUser
    {
        public IMongoQueryable<User> Obtain();

        public IMongoCollection<User> GetCollection();

        public IMongoQueryable<User> GetUserById(string id);

        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate);
    }
}
