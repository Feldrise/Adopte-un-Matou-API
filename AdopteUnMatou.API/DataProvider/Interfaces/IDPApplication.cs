using AdopteUnMatou.API.Models.Applications;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.DataProvider.Interfaces
{
    public interface IDPApplication
    {
        public IMongoQueryable<Application> Obtain();

        public IMongoCollection<Application> GetCollection();

        public IMongoQueryable<Application> GetApplicationById(string id);

        public IMongoQueryable<Application> GetFiltered(Expression<Func<Application, bool>> predicate);
    }
}
