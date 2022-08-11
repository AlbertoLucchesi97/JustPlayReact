using JustPlay;
using JustPlay.Authorization;
using JustPlay.Data;
using JustPlay.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Net;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace JustPlayIntegrationTests.StepDefinitions
{
    [Binding]
    public class WebApiStepDefinitions
    {
        private ScenarioContext _scenarioContext;
        public WebApiStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"an App and a Client")]
        public void GivenAnAppAndAClient()
        {
            var application = new WebApplicationFactory<TestStartup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<JustPlayContext>));
                        services.AddDbContext<JustPlayContext>(options =>
                        {
                            options.UseSqlServer("Server=ALBERTO\\SQLEXPRESS;Database=JustPlayTest;Trusted_Connection=True;TrustServerCertificate=True;");
                        });
                    });
                    builder.UseStartup<TestStartup>();
                });

            var client = application.CreateClient();
             
            client.DefaultRequestHeaders.Add("Authorization", "bearer " +
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkU5YnN4VHdJbHhIZ01BRW94QWpEXyJ9.eyJodHRwczovL2V4YW1wbGUuY29tL2VtYWlsIjoidGVzdEB0ZXN0LmNvbSIsImlzcyI6Imh0dHBzOi8vZGV2LXN0ZGlyNm54LnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2MjgzN2RmMjViYmZhYzAwNjcxNzFlZmUiLCJhdWQiOlsiaHR0cHM6Ly9qdXN0cGxheSIsImh0dHBzOi8vZGV2LXN0ZGlyNm54LnVzLmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE2NTI3ODQ4NjQsImV4cCI6MTY1NTM3Njg2NCwiYXpwIjoiT21lM1pEVE9CRUNyV1RFQ2xSRWFmTDZoVzBneXlaYTgiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIn0.AzREd0H0XyZdQor9zgQgB63NxYgcH61qCDgmPAdY0ORq3YSef-jEwJ05Ndf62JZywz2r3GKNM_o97UZ1aZZE7mL1yGLQkXT9QKKipy6pUuwZQ1ILfS2m2mQefWa_Yjrg3afjfU0AUZhm8hv1-6SGHB0PB2KWm3-BzASyPs4d7R96msKFfuiDN1AXtnvW3M0kkGTcPlgfKNxgfi_zNI-1kJI4U81dHNOLT1xD9ZqiUmM6r5Cv0CSony1ex24-lGGnPl36FWGLOdr8byXtBGI3LwzpVEhb8R160pyi8Qa9GCyiCIVAL8qZJajGToE1Oh7_3KEe20vQTXxe5HBbjR5ABQ");
            
            _scenarioContext["application"] = application;
            _scenarioContext["client"] = client;
        }

        [When(@"I call the Api to get all the videogames")]
        public async Task WhenICallTheApiToGetAllTheVideogames()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("/api/videogames");
        }

        [When(@"I call the Api to get a list of sorted videogames")]
        public async Task WhenICallTheApiToGetAListOfSortedVideogames()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("/api/videogames?sort=title");
        }

        [When(@"I call the Api to get a specific videogame")]
        public async Task WhenICallTheApiToGetASpecificVideogame()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("/api/videogames/2");
        }

        [Then(@"The Status Code should be OK")]
        public void ThenTheStatusCodeShouldBeOK()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When(@"I call the Api to get a specific videogame with a wrong id")]
        public async Task WhenICallTheApiToGetASpecificVideogameWithAWrongId()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("/api/videogames/88");
        }

        [Then(@"The Status Code should be Not Found")]
        public void ThenTheStatusCodeShouldBeNotFound()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [When(@"I call the Api to get a list of searched videogames")]
        public async Task WhenICallTheApiToGetAListOfSearchedVideogames()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("/api/videogames?search=war");
        }

        [When(@"I call the Api to post a videogame")]
        public async Task WhenICallTheApiToPostAVideogame()
        {
            var client = _scenarioContext["client"] as HttpClient;
            var videogame = new Videogame
            {
                Title = "Uncharted 2",
                Year = 2009,
                SoftwareHouse = "Naughty Dog",
                Publisher = "Sony Entertainment",
                Synopsis = "Embark in a new adventure with Nathan Drake!",
                Cover = "https://static.wikia.nocookie.net/uncharted/images/b/be/Among_Thieves_front_cover_%28US%29.png/revision/latest?cb=20200222175639",
                Trailer = "https://www.youtube.com/embed/tlkkceDkT88"
            };
            _scenarioContext["response"] = await client.PostAsJsonAsync("/api/videogames", videogame);
        }

        [Then(@"The Status Code should be Created")]
        public void ThenTheStatusCodeShouldBeCreated()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [When(@"I call the Api to put a videogame")]
        public async Task WhenICallTheApiToPutAVideogame()
        {
            var client = _scenarioContext["client"] as HttpClient;
            var videogameUpdate = new Videogame
            {
                Title = "test",
                Year = 2022,
                Genre = "test",
                SoftwareHouse = "test",
                Publisher = "test",
                Synopsis = "test",
                Cover = "test",
                Trailer = "test"
            };
            _scenarioContext["response"] = await client.PutAsJsonAsync("api/videogames/1", videogameUpdate);
        }

        [When(@"I call the Api to delete a videogame")]
        public async Task WhenICallTheApiToDeleteAVideogame()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.DeleteAsync("api/videogames/3");
        }

        [Then(@"The Status Code should be No Content")]
        public void ThenTheStatusCodeShouldBeNoContent()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [When(@"I call the Api to delete a videogame with a wrong id")]
        public async Task WhenICallTheApiToDeleteAVideogameWithAWrongId()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.DeleteAsync("api/videogames/88");
        }

        [When(@"I call the Api to get a user")]
        public async Task WhenICallTheApiToGetAUser()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("api/users/");
        }

        [When(@"I call the Api to get a user wishlist")]
        public async Task WhenICallTheApiToGetAUserWishlist()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("api/users/videogamesWishlist");
        }

        [When(@"I call the Api to get a user owned videogames")]
        public async Task WhenICallTheApiToGetAUserOwnedVideogames()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.GetAsync("api/users/videogamesOwned");
        }

        [When(@"I call the Api to add a videogame to wishlist")]
        public async Task WhenICallTheApiToAddAVideogameToWishlist()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.PostAsync("api/users/videogamesWishlist/add/7", null);
        }

        [When(@"I call the Api to remove a videogame from wishlist")]
        public async Task WhenICallTheApiToRemoveAVideogameFromWishlist()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.DeleteAsync("api/users/videogamesWishlist/remove/2");
        }

        [When(@"I call the Api to add a videogame to owned")]
        public async Task WhenICallTheApiToAddAVideogameToOwned()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.PostAsync("api/users/videogamesOwned/add/8", null);
        }

        [When(@"I call the Api to remove a videogame from owned")]
        public async Task WhenICallTheApiToRemoveAVideogameFromOwned()
        {
            var client = _scenarioContext["client"] as HttpClient;
            _scenarioContext["response"] = await client.DeleteAsync("api/users/videogamesOwned/remove/5");
        }
    }
}
