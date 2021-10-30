using AdopteUnMatou.API.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services.Interfaces
{
    public interface IApplicationsService
    {
        Task<Application> GetApplication(string id);
        Task<IList<Application>> GetApplications(bool shouldIncludeContent);

        Task<string> CreateApplication(Application toCreate);
        Task UpdateApplication(Application toUpdate);
    }
}
