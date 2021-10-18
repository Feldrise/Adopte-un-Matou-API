using AdopteUnMatou.API.Contexts;
using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Cats;
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
    public class DPCats : IDPCats
    {
        private readonly AdopteUnMatouContext _context;

        public DPCats(IOptions<MongoSettings> settings)
        {
            _context = new AdopteUnMatouContext(settings);
        }

        public IMongoQueryable<Cat> Obtain()
        {
            return _context.Cat.AsQueryable();
        }

        public IMongoCollection<Cat> GetCollection()
        {
            return _context.Cat;
        }

        public IMongoQueryable<Cat> GetCatById(string id)
        {
            return _context.Cat.AsQueryable().Where(dbCat => dbCat.Id == id);
        }

        public IMongoQueryable<Cat> GetFiltered(Expression<Func<Cat, bool>> predicate)
        {
            return _context.Cat.AsQueryable().Where(predicate);
        }
    }
}
