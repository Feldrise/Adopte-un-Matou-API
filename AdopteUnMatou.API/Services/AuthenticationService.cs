using AdopteUnMatou.API.Entities;
using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.API.Services.Interfaces;
using AdopteUnMatou.API.Settings.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMongoCollection<User> _users;

        public AuthenticationService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }

        public async Task<string> RegisterAsync(RegisterModel registerModel)
        {
            // We need some basic checks
            if (string.IsNullOrWhiteSpace(registerModel.Email)) { throw new Exception("An email is required for the registration"); }
            if (UserExist(registerModel.Email)) { throw new Exception("The user already exist"); }
            if (!IsRoleValide(registerModel.Role)) { throw new Exception("The role you registered in is not a valid role"); }

            // Password stuff, to ensure we never have clear password stored
            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Now we register the user in the database
            User dbUser = new()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,

                Email = registerModel.Email.ToLower(),

                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = passwordSalt,

                Role = registerModel.Role
            };

            await _users.InsertOneAsync(dbUser);

            return dbUser.Id;
        }

        private bool UserExist(string email)
        {
            return _users.AsQueryable().Any(dbUser =>
                dbUser.Email == email.ToLower() 
            );
        }

        private bool IsRoleValide(string role)
        {
            return role == Roles.Adoptant;
        }

        // Password related functions
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new Exception("The password must not be null or empty"); }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
