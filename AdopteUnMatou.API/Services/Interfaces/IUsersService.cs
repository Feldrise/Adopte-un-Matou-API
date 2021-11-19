using AdopteUnMatou.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUser(string id);
        Task<IList<User>> GetUsers(bool shouldIncludeSensitiveInfo);

        Task<byte[]> GetUserImage(string id);

        Task UpdateUser(byte[] newImage, User toUpdate);
    }
}
