using AdopteUnMatou.API;
using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.IntegrationTest.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdopteUnMatou.IntegrationTest
{
    public class AuthenticationTests : TestBase
    {
        public AuthenticationTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/authentication/register")]
        public async Task Register(string url)
        {
            /// Arrange
            var client = Factory.CreateClient();

            var registerModel = new RegisterModel
            {
                FirstName = "Victor",
                LastName = "DENIS",
                Email = "admin@feldrise.com",

                Password = "MySecurePassword",

                Role = "Adoptant"
            };

            /// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json"));

            /// Assert
            response.EnsureSuccessStatusCode();
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/authentication/register", "test1@gmail.com", "Adoptant")]
        [InlineData("/api/authentication/register", "contact@feldrise.com", "InvalideRole")]
        public async Task Register_Failure(string url, string email, string role)
        {
            /// Arrange
            var client = Factory.CreateClient();

            var registerModel = new RegisterModel
            {
                FirstName = "Victor",
                LastName = "DENIS",
                Email = email,

                Password = "MySecurePassword",

                Role = role
            };

            /// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json"));

            /// Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
