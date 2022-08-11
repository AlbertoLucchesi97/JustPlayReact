using JustPlay.Controllers;
using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JustPlayTests
{
    public class UsersControllerTests
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
        public async void GetUserByEmail_WithValidEmail_ReturnsUser()
        {
            var mockUser = new User
            {
                ID = 1,
                Email = "test@test.com",
                Admin = false
            };
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.GetUserByEmail(null))
                .Returns(() => Task.FromResult(mockUser));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.GetUserByEmail();
            Assert.Equal(mockUser, actualResult.Value);
        }

        [Fact]
        public async void GetUserVideogamesOwned_ReturnsVideogamesList()
        {
            var mockVideogameList = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.GetVideogamesOwned(null)).Returns(() => Task.FromResult(mockVideogameList.AsEnumerable()));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));


            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.GetUserVideogamesOwned();
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogameList, actualResult);
        }

        [Fact]
        public async void GetUserVideogamesWishlist_ReturnsVideogamesList()
        {
            var mockVideogameList = createVideogamesList();

            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.GetVideogamesWishlist(null)).Returns(() => Task.FromResult(mockVideogameList.AsEnumerable()));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.GetUserVideogamesWishlist();
            Assert.Equal(10, actualResult.Count());
            Assert.Equal(mockVideogameList, actualResult);
        }

        [Fact]
        public async void AddVideogameToWishList_WithValidParameter_ReturnsVideogameId()
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.AddVideogameToWishlist(null, 1));
            
            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.AddVideogamesToWishlist(1);
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async void AddVideogameInOwned_WithValidParameter_ReturnsVideogameId() 
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.AddVideogameToOwned(null, 1));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.AddVideogamesToOwned(1);
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async void RemoveVideogameFromWishlist_WithValidParameter_ReturnsVideogameId()
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.RemoveVideogameFromWishlist(null, 1));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.RemoveVideogamesFromWishlist(1);
            Assert.Equal(1, actualResult);
        }

        [Fact]
        public async void RemoveVideogameFromOwned_WithValidParameter_ReturnsVideogameId()
        {
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository.Setup(r => r.RemoveVideogameFromOwned(null, 1));

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("", ""));

            var usersController = new UsersController(mockDataRepository.Object, mockHttpAccessor.Object);

            var actualResult = await usersController.RemoveVideogamesFromOwned(1);
            Assert.Equal(1, actualResult);
        }
    }
}
