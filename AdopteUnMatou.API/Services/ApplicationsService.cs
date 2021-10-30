using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Applications;
using AdopteUnMatou.API.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services
{
    public class ApplicationsService : IApplicationsService
    {
        private readonly IDPApplication _dpApplication;

        public ApplicationsService(IDPApplication dPApplication)
        {
            _dpApplication = dPApplication;
        }

        public async Task<Application> GetApplication(string id)
        {
            return await _dpApplication.GetApplicationById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<Application>> GetApplications(bool shouldIncludeContent)
        {
            IMongoQueryable<Application> applications = _dpApplication.Obtain();

            var applicationsList = await applications.ToListAsync();

            if (!shouldIncludeContent)
            {
                foreach (var application in applicationsList)
                {
                    application.Questions = null;
                }
            }

            return applicationsList;
        }
        public async Task<string> CreateApplication(Application toCreate)
        {
            await _dpApplication.GetCollection().InsertOneAsync(toCreate);

            return toCreate.Id;
        }

        public async Task UpdateApplication(Application toUpdate)
        {
            await _dpApplication.GetCollection().ReplaceOneAsync(
                dbApplication => dbApplication.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
