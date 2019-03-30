using DatabaseIntegration.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DatabaseIntegration.IntegrationTest
{
    public class BooksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public BooksControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            var httpResponse = await _httpClient.GetAsync("/api/v1/books");

            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IList<Book>>(response);

            Assert.NotEmpty(actual);
            Assert.True(actual[0].Id > 0);
        }
    }
}
