using JustPlay.Data;
using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustPlayIntegrationTests.StepDefinitions
{
    [Binding]
    public class CommonStepDefinitions
    {
        JustPlayContext context;
        ScenarioContext scenarioContext;

        public CommonStepDefinitions(ScenarioContext _scenarioContext)
        {
            scenarioContext = _scenarioContext;
        }

        
        [BeforeScenario]
        public void CreateDb()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<JustPlayContext>();
            builder.UseSqlServer("Server=ALBERTO\\SQLEXPRESS;Database=JustPlayTest;Trusted_Connection=True;TrustServerCertificate=True;");

            context = new JustPlayContext(builder.Options);
            
            context.Database.EnsureCreated();

            var videogames = new Videogame[]
            {
                new Videogame{Title = "Gran Turismo 7", Year = 2022, Genre="Racing",
                    SoftwareHouse = "Poliphony Diital", Publisher = "Sony", Synopsis = "Find your line.", Cover = "*", Trailer =""},
                new Videogame{Title = "Forza Horizon 5", Year = 2021, Genre="Racing",
                    SoftwareHouse="Playground Games", Publisher="Microsoft Game Studios", Synopsis="Race in Mexico and esplore the map!", Cover="*", Trailer ="" },
                new Videogame{Title = "Elden Ring", Year = 2022, Genre="RPG",
                    SoftwareHouse = "From Software", Publisher = "Namco Bandai", Synopsis = "Kill the bosses", Cover = "*", Trailer =""},
                new Videogame{Title = "Dark Souls 3", Year = 2016, Genre="RPG",
                    SoftwareHouse = "From Software", Publisher = "Namco Bandai", Synopsis = "Kill the bosses", Cover = "*", Trailer =""},
                new Videogame{Title = "Life is Strange", Year = 2015, Genre="Adventure",
                    SoftwareHouse = "Dontnod", Publisher = "Square Enix", Synopsis = "You are Max, save Chloe.", Cover = "*", Trailer =""},
                new Videogame{Title = "Life is Strange True Colors", Year = 2021, Genre="Adventure",
                    SoftwareHouse = "Deck Nine", Publisher = "Square Enix", Synopsis = "Find the story about your brother's death", Cover = "*", Trailer =""}
            };

            var users = new User[]
 {
                new User{Email = "albertolucchesi97@gmail.com", Admin = false},
                new User{Email = "test@test.com", Admin = true, AuthId = "auth0|62837df25bbfac0067171efe"} //password Test123@
 };

            var videogamesWishlist = new VideogameWishlist[]
{
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 1},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 2},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 3},
};

            var videogamesOwned = new VideogameOwned[]
            {
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 4},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 5},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 6},
            };


            context.Videogames.AddRange(videogames);
            context.SaveChanges();

            context.Users.AddRange(users);
            context.SaveChanges();

            context.VideogamesOwned.AddRange(videogamesOwned);
            context.SaveChanges();

            context.VideogamesWishlist.AddRange(videogamesWishlist);
            context.SaveChanges();

            scenarioContext["repository"] = new DataRepository(context);
            scenarioContext["httpAccessor"] = new HttpContextAccessor();
        }

        [AfterScenario]
        public void DeleteDb()
        {
            context.Database.EnsureDeleted();
        }
    }
}
