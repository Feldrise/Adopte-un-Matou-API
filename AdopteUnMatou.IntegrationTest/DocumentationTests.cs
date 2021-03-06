using AdopteUnMatou.API;
using AdopteUnMatou.IntegrationTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdopteUnMatou.IntegrationTest
{
    public class DocumentationTests : TestBase
    {
        public DocumentationTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/documentation/index.html")]
        public async Task Get_Documentation(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(true);
        }
    }
}
