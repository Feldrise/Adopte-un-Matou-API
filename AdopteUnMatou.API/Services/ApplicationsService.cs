using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Applications;
using AdopteUnMatou.API.Models.Cats;
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
        private readonly IDPCats _dpCats;

        public ApplicationsService(IDPApplication dPApplication, IDPCats dPCats)
        {
            _dpApplication = dPApplication;
            _dpCats = dPCats;
        }

        public async Task<Application> GetApplication(string id)
        {
            return await _dpApplication.GetApplicationById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<Application>> GetApplications(bool shouldIncludeContent, string userId)
        {
            IMongoQueryable<Application> applications;

            if (!string.IsNullOrEmpty(userId))
            {
                applications = _dpApplication.GetFiltered(dbApplication => dbApplication.UserId == userId);
            }
            else
            {
                applications = _dpApplication.Obtain();
            }

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
            if (toUpdate.ApplicationStep != ApplicationSteps.Sent)
            {
                Cat cat = await _dpCats.GetCatById(toUpdate.CatId).FirstOrDefaultAsync();

                if (cat != null)
                {
                    var catUpdate = Builders<Cat>.Update
                        .Set(dbCat => dbCat.AdoptionStatus, AdoptionStatus.Reserved);

                    if (toUpdate.ApplicationStep == ApplicationSteps.Finished)
                    {
                        catUpdate = Builders<Cat>.Update
                            .Set(dbCat => dbCat.AdoptionStatus, AdoptionStatus.Adopted);
                    }

                    if (toUpdate.ApplicationStep == ApplicationSteps.Refused)
                    {
                        catUpdate = Builders<Cat>.Update
                            .Set(dbCat => dbCat.AdoptionStatus, AdoptionStatus.Waiting);
                    }

                    await _dpCats.GetCollection().UpdateOneAsync(
                        dbCat => dbCat.Id == cat.Id,
                        catUpdate
                    ); 
                }
            }

            await _dpApplication.GetCollection().ReplaceOneAsync(
                dbApplication => dbApplication.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
