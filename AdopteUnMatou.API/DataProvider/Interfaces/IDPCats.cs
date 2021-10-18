using AdopteUnMatou.API.Models.Cats;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.DataProvider.Interfaces
{
    public interface IDPCats
    {
        public IMongoQueryable<Cat> Obtain();

        public IMongoCollection<Cat> GetCollection();

        public IMongoQueryable<Cat> GetCatById(string id);

        public IMongoQueryable<Cat> GetFiltered(Expression<Func<Cat, bool>> predicate);
    }
}
