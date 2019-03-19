using DatabaseIntegration.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatabaseIntegration.IntegrationTest
{
    public class PersonControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public PersonControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldCreateNewPerson()
        {
            var person = new Person
            {
                FirstName = "alan",
                LastName = "morais",
                Address = "rua ernesto evans 578",
                Gender = "Male"
            };
            var body = new StringContent(JsonConvert.SerializeObject(person).ToString());
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponse = await _httpClient.PostAsync("/api/persons", body);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadAsStringAsync();
            var personCreated = JsonConvert.DeserializeObject<Person>(response);

            Assert.True(personCreated.Id > 0);
            Assert.Equal(personCreated.FirstName, person.FirstName);
            Assert.Equal(personCreated.LastName, person.LastName);
            Assert.Equal(personCreated.Gender, person.Gender);
            Assert.Equal(personCreated.Address, person.Address);

        }
    }
}
