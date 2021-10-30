using AdopteUnMatou.API.Contexts;
using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Applications;
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
    public class DPApplication : IDPApplication
    {
        private readonly AdopteUnMatouContext _context;

        public DPApplication(IOptions<MongoSettings> settings)
        {
            _context = new AdopteUnMatouContext(settings);
        }

        public IMongoQueryable<Application> Obtain()
        {
            return _context.Application.AsQueryable();
        }

        public IMongoCollection<Application> GetCollection()
        {
            return _context.Application;
        }

        public IMongoQueryable<Application> GetApplicationById(string id)
        {
            return _context.Application.AsQueryable().Where(dbApplication => dbApplication.Id == id);
        }

        public IMongoQueryable<Application> GetFiltered(Expression<Func<Application, bool>> predicate)
        {
            return _context.Application.AsQueryable().Where(predicate);
        }
    }
}
