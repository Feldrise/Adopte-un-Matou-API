using AdopteUnMatou.API.Contexts;
using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Entities;
using AdopteUnMatou.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.DataProvider
{
    public class DPUser : IDPUser
    {
        private readonly AdopteUnMatouContext _context;

        public DPUser(IOptions<MongoSettings> settings)
        {
            _context = new AdopteUnMatouContext(settings);
        }

        public IMongoQueryable<User> Obtain()
        {
            return _context.User.AsQueryable();
        }

        public IMongoCollection<User> GetCollection()
        {
            return _context.User;
        }

        public IMongoQueryable<User> GetUserById(string id)
        {
            return _context.User.AsQueryable().Where(dbUser => dbUser.Id == id);
        }

        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate)
        {
            return _context.User.AsQueryable().Where(predicate);
        }
    }
}
