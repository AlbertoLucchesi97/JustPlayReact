using JustPlay.Controllers;
using JustPlay.Data;
using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace JustPlayIntegrationTests.StepDefinitions
{
    [Binding]
    public class VideogamesControllerStepDefinitions
    {
        private ScenarioContext _scenarioContext;

        VideogamesController videoGamesController;

        public VideogamesControllerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the VideogamesController")]
        public void GivenTheVideogamesController()
        {
            var repository = _scenarioContext["repository"] as IDataRepository;
            videoGamesController = new VideogamesController(repository);
        }

        [When(@"I query all videogames")]
        public async Task WhenIQueryAllVideogames()
        {
            _scenarioContext["videogamesList"] = await videoGamesController.GetAllVideogames(null, null);
        }
        
        [Then(@"the Result should be a Task with a list of Videogames")]
        public void ThenTheResultShouldBeAListOfVideogames()
        {
            var videogamesList = _scenarioContext["videogamesList"] as IEnumerable<Videogame>;
            Assert.IsNotNull(videogamesList);
            Assert.IsInstanceOfType(videogamesList, typeof(IEnumerable<Videogame>));
        }

        [When(@"I query all videogames by searching for a videogame with a valid parameter")]
        public async Task WhenIQueryAllVideogamesBySearchingForAVideogameWithAValidParameter()
        {
            _scenarioContext["videogamesList"] = await videoGamesController.GetAllVideogames("Gran", null);
        }

        [Then(@"the Result should be a Task with a list of the found Videogames")]
        public void ThenTheResultShouldBeATaskWithAListOfTheFoundVideogames()
        {
            var videogamesList = _scenarioContext["videogamesList"] as IEnumerable<Videogame>;
            Assert.IsNotNull(videogamesList);
            Assert.IsInstanceOfType(videogamesList, typeof(IEnumerable<Videogame>));
            Assert.AreEqual(1, videogamesList.Count());
        }

        [When(@"I query all videogames by searching for a videogame with an invalid parameter")]
        public async Task WhenIQueryAllVideogamesBySearchingForAVideogameWithAnInvalidParameter()
        {
            _scenarioContext["videogamesList"] = await videoGamesController.GetAllVideogames("undefined", null);
        }

        [Then(@"the Result should be a Task with an empty list of found Videogames")]
        public void ThenTheResultShouldBeATaskWithAnEmptyListOfFoundVideogames()
        {
            var videogamesList = _scenarioContext["videogamesList"] as IEnumerable<Videogame>;
            Assert.IsNotNull(videogamesList);
            Assert.IsInstanceOfType(videogamesList, typeof(IEnumerable<Videogame>));
            Assert.AreEqual(0, videogamesList.Count());
        }

        [When(@"I query all videogames with a null search and valid sort parameter")]
        public async Task WhenIQueryAllVideogamesWithANullSearchAndValidSortParameter()
        {
            _scenarioContext["videogamesList"] = await videoGamesController.GetAllVideogames(null, "title");
        }

        [Then(@"the Result should be a Task with a sorted list of the found Videogames")]
        public void ThenTheResultShouldBeATaskWithASortedListOfTheFoundVideogames()
        {
            var videogamesList = _scenarioContext["videogamesList"] as IEnumerable<Videogame>;
            Assert.IsNotNull(videogamesList);
            Assert.IsInstanceOfType(videogamesList, typeof(IEnumerable<Videogame>));
            Assert.AreEqual("Dark Souls 3", videogamesList.First().Title);
        }


        [When(@"I query a videogame with a working id")]
        public async Task WhenIQueryAVideogameWithAWorkingId()
        {
            _scenarioContext["videogame"] = await videoGamesController.GetVideogame(5);
        }
        
        [Then(@"the Result should be a Task with the found Videogame")]
        public void ThenTheResultShouldBeATaskWithTheFoundVideogame()
        {
            var expectedVideogame = new Videogame
            {
                ID = 5,
                Title = "Life is Strange",
                Year = 2015,
                Genre = "Adventure",
                SoftwareHouse = "Dontnod",
                Publisher = "Square Enix",
                Synopsis = "You are Max, save Chloe.",
                Cover = "*",
                Trailer = ""
            };

            var videogame = _scenarioContext["videogame"] as ActionResult<Videogame>;
            Assert.IsNotNull(videogame);
            Assert.IsInstanceOfType(videogame, typeof(ActionResult<Videogame>));
            Assert.AreEqual(expectedVideogame, videogame.Value);
        }

        [When(@"I query a videogame with a non working id")]
        public async Task WhenIQueryAVideogameWithANonWorkingId()
        {
            _scenarioContext["videogame"] = await videoGamesController.GetVideogame(0);
        }
        
        [Then(@"the Result of GetVideogame should be a Task with a NotFound message")]
        public void ThenTheResultOfGetVideogameShouldBeATaskWithANotFoundMessage()
        {
            var videogame = _scenarioContext["videogame"] as ActionResult<Videogame>;
            Assert.IsInstanceOfType(videogame.Result, typeof(NotFoundResult));
        }

        [When(@"I post a videogame with a videogame instance")]
        public async Task WhenIPostAVideogameWithAVideogameInstance()
        {
            var videogameToPost = new Videogame()
            {
                Title = "The Last of Us",
                Year = 2013,
                Genre = "Adventure",
                SoftwareHouse = "Naughty Dog",
                Publisher = "Sony",
                Synopsis = "Bring Ellie to the Fireflies",
                Cover = "*",
                Trailer = ""
            };

            _scenarioContext["postedVideogame"] = await videoGamesController.PostVideogame(videogameToPost);
        }
        
        [Then(@"the Result should be a Task with a ActionResult of Videogame")]
        public void ThenTheResultShouldBeATaskWithThePostedVideogame()
        {
            var postedVideogame = _scenarioContext["postedVideogame"] as ActionResult<Videogame>;
            Assert.IsNotNull(postedVideogame);
            Assert.IsInstanceOfType(postedVideogame, typeof(ActionResult<Videogame>));
        }

        [When(@"I post a videogame with a null parameter")]
        public async Task WhenIPostAVideogameWithANullParameter()
        {
            _scenarioContext["postedVideogame"] = await videoGamesController.PostVideogame(null);
        }
        
        [Then(@"the Result of PostVideogame should be a Task with a BadRequest")]
        public void ThenTheResultOfPostVideogameShouldBeATaskWithABadRequestMessage()
        {
            var postedVideogame = _scenarioContext["postedVideogame"] as ActionResult<Videogame>;
            Assert.IsInstanceOfType(postedVideogame.Result, typeof(BadRequestResult));
        }

        [When(@"I call the PutVideogame method with a valid videogame id and a videogame instance")]
        public async Task WhenICallThePutVideogameMethodWithAValidVideogameIdAndAVideogameInstance()
        {
            var videogame = new Videogame
            {
                Title = "Gran Turismo 7",
                Year = 2022,
                Genre = "Racing",
                SoftwareHouse = "Poliphony Diital",
                Publisher = "Sony",
                Synopsis = "Race against the others!",
                Cover = "*",
                Trailer = ""
            };

            _scenarioContext["putVideogame"] = await videoGamesController.PutVideogame(1, videogame);
        }
        
        [Then(@"the Result should be a Task with the UpdatedVideogame")]
        public void ThenTheResultShouldBeATaskWithTheUpdatedVideogame()
        {
            var expectedVideogame = new Videogame
            {
                Title = "Gran Turismo 7",
                Year = 2022,
                Genre = "Racing",
                SoftwareHouse = "Poliphony Diital",
                Publisher = "Sony",
                Synopsis = "Race against the others!",
                Cover = "*",
                Trailer = ""
            };

            var putVideogame = _scenarioContext["putVideogame"] as ActionResult<Videogame>;
            Assert.IsNotNull(putVideogame);
            Assert.IsInstanceOfType(putVideogame, typeof(ActionResult<Videogame>));
            Assert.AreEqual(expectedVideogame, putVideogame.Value);
        }

        [When(@"I call the PutVideogame method with a non working id and a null parameter")]
        public async Task WhenICallThePutVideogameMethodWithANonWorkingIdAndANullParameter()
        {
            _scenarioContext["putVideogame"] = await videoGamesController.PutVideogame(0, null);
        }
        
        [Then(@"the Result of PutVideogame should be a Task with NotFound error")]
        public void ThenTheResultOfPutVideogameShouldBeATaskWithNotFoundError()
        {
            var putVideogame = _scenarioContext["putVideogame"] as ActionResult<Videogame>;
            Assert.IsInstanceOfType(putVideogame.Result, typeof(NotFoundResult));
        }

        [When(@"I call the DeleteVideogame method with a valid videogame id")]
        public async Task WhenICallTheDeleteVideogameMethodWithAValidVideogameId()
        {
            _scenarioContext["deleteVideogame"] = await videoGamesController.DeleteVideogame(2);
        }
        
        [Then(@"the Result should be a Task with a NoContent")]
        public void ThenTheResultShouldBeATaskWithANoContent()
        {
            var deleteVideogame = _scenarioContext["deleteVideogame"] as ActionResult;
            Assert.IsInstanceOfType(deleteVideogame, typeof(NoContentResult));
        }

        [When(@"I call the DeleteVideogame method with a non working id")]
        public async Task WhenICallTheDeleteVideogameMethodWithANonWorkingId()
        {
            _scenarioContext["deleteVideogame"] = await videoGamesController.DeleteVideogame(10);
        }

        [Then(@"the Result of DeleteVideogame should be a Task with a NotFound")]
        public void ThenTheResultOfDeleteVideogameShouldBeATaskWithANotFound()
        {
            var deleteVideogame = _scenarioContext["deleteVideogame"] as ActionResult;
            Assert.IsInstanceOfType(deleteVideogame, typeof(NotFoundResult));
        }
    }
}
