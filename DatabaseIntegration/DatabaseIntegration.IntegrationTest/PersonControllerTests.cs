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
        private Person _person;

        public PersonControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();

            _person = new Person
            {
                FirstName = "alan",
                LastName = "morais",
                Address = "rua ernesto evans 578",
                Gender = "Male"
            };
        }

        [Fact]
        public async Task ShouldCreateNewPerson()
        {
            var body = new StringContent(JsonConvert.SerializeObject(_person).ToString());
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponse = await _httpClient.PostAsync("/api/v1/persons", body);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadAsStringAsync();
            var personCreated = JsonConvert.DeserializeObject<Person>(response);

            Assert.True(personCreated.Id > 0);
            Assert.Equal(personCreated.FirstName, _person.FirstName);
            Assert.Equal(personCreated.LastName, _person.LastName);
            Assert.Equal(personCreated.Gender, _person.Gender);
            Assert.Equal(personCreated.Address, _person.Address);

        }

        [Fact]
        public async Task ShouldGetPersonById()
        {
            var httpResponse = await _httpClient.GetAsync("/api/v1/persons/1");
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadAsStringAsync();
            var personCreated = JsonConvert.DeserializeObject<Person>(response);

            Assert.True(personCreated.Id.Equals(1));
            Assert.NotEmpty(personCreated.FirstName);
            Assert.NotEmpty(personCreated.LastName);
            Assert.NotEmpty(personCreated.Gender);
            Assert.NotEmpty(personCreated.Address);
        }

        [Fact]
        public async Task ShouldDeleteById()
        {
            var httpResponse = await _httpClient.DeleteAsync("/api/v1/persons/2"); 
            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldUpdate()
        {
            _person.LastName = "Fialho";
            var body = new StringContent(JsonConvert.SerializeObject(_person).ToString());
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponse = await _httpClient.PutAsync("/api/v1/persons/1", body);
            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
