using JustPlay.Controllers;
using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JustPlayTests
{
    public class VideogamesControllerTests
    {
        public List<Videogame> createVideogamesList()
        {
            var mockVideogames = new List<Videogame>();
            for (int i = 1; i <= 10; i++)
            {
                mockVideogames.Add(new Videogame
                {
                    ID = i,
                    Title = $"Title test {i}",
                    Year = 2020 + i,
                    Genre = "Genre test",
                    SoftwareHouse = $"Test SoftwareHouse {i}",
                    Publisher = $"Test Publisher {i}",
                    Synopsis = $"Test Synopsis {i}",
                    Cover = $"Test Cover {i}",
                    Trailer = ""
                });
            }
            return mockVideogames;
        }

        [Fact]
        public async void GetAllVideogames_ReturnsAllVideogames()
        {
            var mockVideogames = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames(null, null))
                .Returns(() => Task.FromResult(mockVideogames.AsEnumerable()));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames(null, null);
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogames, actualResult);
        }

        [Fact]
        public async void GetVideogamesBySearching_WithValidParameter_ReturnsVideogames()
        {
            var mockVideogames = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames("test", null))
                .Returns(() => Task.FromResult(mockVideogames.AsEnumerable()));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames("test", null);
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogames, actualResult);
        }

        [Fact]
        public async void GetVideogamesBySearching_WithInvalidParameter_ReturnsEmptyList()
        {
            var expectedReturnList  = new List<Videogame>();
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames("undefined", null))
                .Returns(() => Task.FromResult(expectedReturnList.AsEnumerable()));
            
            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames("undefined", null);
            Assert.Empty(actualResult);
            Assert.Equal(expectedReturnList, actualResult);
        }

        [Fact]
        public async void GetVideogames_WithNullSearchAndValidSortYear_ReturnsFilteredList()
        {
            var mockVideogamesList = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames(null, "year"))
                .Returns(() => Task.FromResult(mockVideogamesList.OrderBy(t => t.Year)
                .AsEnumerable()));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames(null, "year");
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogamesList.OrderBy(t => t.Year), actualResult);
        }

        [Fact]
        public async void GetVideogames_WithNullSearchAndValidSortTitle_ReturnsFilteredList()
        {
            var mockVideogamesList = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames(null, "title"))
                .Returns(() => Task.FromResult(mockVideogamesList.OrderBy(t => t.Title)
                .AsEnumerable()));
            
            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames(null, "title");
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogamesList.OrderBy(t => t.Title), actualResult);
        }

        [Fact]
        public async void GetVideoGames_WithNullSearchAndNullSort_ReturnsUnfilteredList()
        {
            var mockVideogamesList = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetAllVideogames(null, null))
                .Returns(() => Task.FromResult(mockVideogamesList.AsEnumerable()));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetAllVideogames(null, null);
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogamesList, actualResult);
        }    
        
        [Fact]
        public async void GetVideogame_WithValidVideogameId_ReturnsAVideogame()
        {
            var mockVideogame = new Videogame 
            { 
                ID = 1, 
                Title = "Title test 1", 
                Year = 2022, 
                Genre="Genre test", 
                SoftwareHouse = "Software House test 1", 
                Publisher = "Publisher test 1", 
                Synopsis = "Synopsis test 1", 
                Cover = "Cover test 1",
                Trailer = ""
            };
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetVideogame(1))
                .Returns(() => Task.FromResult(mockVideogame));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetVideogame(1);
            Assert.Equal(mockVideogame, actualResult.Value);
        }

        [Fact]
        public async void GetVideogame_WithoutId_ReturnsError()
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetVideogame(1))
                .Returns(() => Task.FromResult(default(Videogame)));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.GetVideogame(1);
            var actionResult = actualResult.Result;
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void PostVideogame_WithVideogameInstance_ReturnTask()
        {
            var mockVideogame = new Videogame
            {
                ID = 1,
                Title = "Title test 1",
                Year = 2022,
                Genre = "Genre test",
                SoftwareHouse = "Software House test 1",
                Publisher = "Publisher test 1",
                Synopsis = "Synopsis test 1",
                Cover = "Cover test 1",
                Trailer = ""
            };

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.PostVideogame(mockVideogame))
                .Returns(Task.FromResult(mockVideogame));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.PostVideogame(mockVideogame);
            Assert.IsType<ActionResult<Videogame>>(actualResult);
        }

        [Fact]
        public async void PostVideogame_WithInvalidParameter_ReturnBadRequest()
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.PostVideogame(null))
                .Returns(Task.FromResult(default(Videogame)));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.PostVideogame(null);
            Assert.IsType<BadRequestResult>(actualResult.Result);
        }

        [Fact]
        public async void PutVideogame_WithValidParameter_ReturnUpdatedQuestion()
        {
            var mockVideogame = new Videogame
            {
                ID = 1,
                Title = "Title test 1",
                Year = 2022,
                Genre = "Genre test",
                SoftwareHouse = "Software House test 1",
                Publisher = "Publisher test 1",
                Synopsis = "Synopsis test 1",
                Cover = "Cover test 1",
                Trailer = ""
            };

            var mockVideogameUpdated = new Videogame
            {
                ID = 1,
                Title = "Title test 1 Updated",
                Year = 2022,
                Genre = "Genre test Updated",
                SoftwareHouse = "Software House test 1 Updated",
                Publisher = "Publisher test 1 Updated",
                Synopsis = "Synopsis test 1 Updated",
                Cover = "Cover test 1 Updated",
                Trailer = ""
            };

            var mockDataRepository = new Mock<IDataRepository>();

            mockDataRepository
                .Setup(r => r.GetVideogame(1))
                .Returns(Task.FromResult(mockVideogame));
            mockDataRepository
                .Setup(r => r.PutVideogame(mockVideogameUpdated, mockVideogame))
                .Returns(Task.FromResult(mockVideogameUpdated));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.PutVideogame(1, mockVideogameUpdated);

            Assert.Equal(mockVideogameUpdated, actualResult.Value);
            Assert.IsType<ActionResult<Videogame>>(actualResult);
        }

        [Fact]
        public async void DeleteVideogame_WithValidInstance_ReturnWithNoContent()
        {
            var mockVideogame = new Videogame
            {
                ID = 1,
                Title = "Title test 1",
                Year = 2022,
                Genre = "Genre test",
                SoftwareHouse = "Software House test 1",
                Publisher = "Publisher test 1",
                Synopsis = "Synopsis test 1",
                Cover = "Cover test 1",
                Trailer = ""
            };

            var mockDataRepository = new Mock<IDataRepository>();

            mockDataRepository
                .Setup(r => r.GetVideogame(1))
                .Returns(Task.FromResult(mockVideogame));

            mockDataRepository
                .Setup(r => r.DeleteVideogame(mockVideogame));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.DeleteVideogame(1);

            Assert.IsType<NoContentResult>(actualResult);
        }

        [Fact]
        public async void DeleteVideogame_WithNullParameter_ReturnsNotFound()
        {
            var mockDataRepository = new Mock<IDataRepository>();

            mockDataRepository
                .Setup(r => r.GetVideogame(1))
                .Returns(Task.FromResult(default(Videogame)));

            var videogamesController = new VideogamesController(mockDataRepository.Object);

            var actualResult = await videogamesController.DeleteVideogame(1);

            Assert.IsType<NotFoundResult>(actualResult);
        }
    }
}
