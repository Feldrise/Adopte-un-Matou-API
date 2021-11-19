using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.API.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IDPUser _dpUsers;

        private readonly IFilesService _filesService;

        public UsersService(IDPUser dPUsers, IFilesService filesService)
        {
            _dpUsers = dPUsers;

            _filesService = filesService;
        }

        public async Task<User> GetUser(string id)
        {
            return await _dpUsers.GetUserById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetUsers(bool shouldIncludeSensitiveInfo)
        {
            IMongoQueryable<User> users = _dpUsers.Obtain();

            var usersList = await users.ToListAsync();

            if (!shouldIncludeSensitiveInfo)
            {
                foreach (var user in usersList)
                {
                    user.Email = null;
                    user.Role = null;
                    user.Token = null;
                }
            }

            return usersList;
        }

        public async Task<byte[]> GetUserImage(string id)
        {
            User user = await _dpUsers.GetUserById(id).FirstOrDefaultAsync();

            if (user == null || user.ProfilePictureId == null) return null;

            return (await _filesService.GetFileById(user.ProfilePictureId)).Data;

        }

        public async Task UpdateUser(byte[] newImage, User toUpdate)
        {
            User oldUser = await _dpUsers.GetUserById(toUpdate.Id).FirstOrDefaultAsync();

            if (oldUser == null)
            {
                throw new Exception("The user doesn't exist");
            }

            if (newImage != null)
            {
                string imageId = await _filesService.UploadFile(toUpdate.Id.ToLower().Trim(), newImage, false);

                toUpdate.ProfilePictureId = imageId;
            }
            else
            {
                toUpdate.ProfilePictureId = oldUser.ProfilePictureId;
            }

            await _dpUsers.GetCollection().ReplaceOneAsync(
                dbUser => dbUser.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
