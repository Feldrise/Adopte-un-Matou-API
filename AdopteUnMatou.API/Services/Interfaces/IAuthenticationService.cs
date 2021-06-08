using AdopteUnMatou.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(RegisterModel registerModel);
    }
}
