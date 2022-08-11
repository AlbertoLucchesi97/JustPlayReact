using JustPlay.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace JustPlay.Data
{
    public static class DbInitializer
    {
        public static void InitializePopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<JustPlayContext>());
            }
        }

        public static void SeedData(JustPlayContext context)
        {
            var videogames = new Videogame[]
            {
                new Videogame{Title = "Gran Turismo 7", Year = 2022, Genre = "Racing",
                    SoftwareHouse = "Poliphony Diital", Publisher = "Sony", Synopsis = "Find your line.", Cover = "https://static-it.gamestop.it/images/products/305561/3max.jpg",
                    Trailer="https://www.youtube.com/embed/1tBUsXIkG1A"},
                new Videogame{Title = "Elden Ring", Year = 2022, Genre = "GDR",
                    SoftwareHouse = "From Software", Publisher = "Namco Bandai", Synopsis = "Kill the bosses", Cover = "https://static-it.gamestop.it/images/products/304042/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/AKXiKBnzpBQ"},
                new Videogame{Title = "Life is Strange", Year = 2015, Genre = "Adventure",
                    SoftwareHouse = "Dontnod", Publisher = "Square Enix", Synopsis = "You are Max, save Chloe.", Cover = "https://static-it.gamestop.it/images/products/268278/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/mpRhaXfvG_0"},
                new Videogame{Title = "The Last of Us", Year = 2013, Genre = "Adventure",
                    SoftwareHouse = "Naughty Dog", Publisher = "Sony Entertainment", Synopsis = "Joel, a smuggler with a dark past, must bring Ellie to the Fireflies.",
                    Cover = "https://static-it.gamestop.it/images/products/251866/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/OQpdSVF_k_w"},
                new Videogame{Title = "The Last of Us Part 2", Year = 2020, Genre = "Adventure",
                    SoftwareHouse = "Naughty Dog", Publisher = "Sony Entertainment",
                    Synopsis = "Avenge the death of Joel in the sequel of the acclaimed The Last of Us. You will have to travel to Boston to find the murdered of Ellie's savior.",
                    Cover = "https://static-it.gamestop.it/images/products/291392/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/eOiUtRF8k28"},
                new Videogame{Title = "Life is Strange True Colors", Year = 2021, Genre = "Adventure",
                    SoftwareHouse = "Deck Nine", Publisher = "Square Enix", Synopsis = "Find the truth about the death of your brother Gabe, and investigate with your friends",
                    Cover = "https://static-it.gamestop.it/images/products/303041/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/EF3nnJMwsBA"},
                new Videogame{Title = "Forza Horizon 4", Year = 2018, Genre = "Racing",
                    SoftwareHouse = "Playground Games", Publisher = "Microsoft Game Studios", Synopsis = "Race and explore in this recreation of the United Kingdom, and become the fastest",
                    Cover = "https://static-it.gamestop.it/images/products/287977/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/5xy4n73WOMM"},
                new Videogame{Title = "Splinter Cell", Year = 2002, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "You are Sam Fisher, the first Splinter Cell of the new NSA department Third Echelon",
                    Cover = "https://www.mobygames.com/images/covers/l/467965-tom-clancy-s-splinter-cell-playstation-2-front-cover.jpg",
                    Trailer = "https://www.youtube.com/embed/Tkt2VRfhg0A"},
                new Videogame{Title = "Splinter Cell Pandora Tomorrow", Year = 2004, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "This is the second Splinter Cell game in the series, and the sequel to the 2002 game.",
                    Cover = "https://www.mondoxbox.com/images/copertine/big/387.jpg",
                    Trailer = "https://www.youtube.com/embed/ZugHKNzM4bw"},
                new Videogame{Title = "Splinter Cell Chaos Theory", Year = 2005, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "Get again in the role of Sam Fisher, and help the NSA to stop a menace against the America",
                    Cover = "https://www.mobygames.com/images/covers/l/511398-tom-clancy-s-splinter-cell-chaos-theory-xbox-manual.jpg",
                    Trailer = "https://www.youtube.com/embed/qTz406fAWyM"},
                new Videogame{Title = "Splinter Cell Double Agent", Year = 2006, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "This time Sam Fisher will have a difficult mission, he will have to go undercover in a terrorist group",
                    Cover = "https://static-it.gamestop.it/images/products/135585/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/G-gMyrll1uo"},
                new Videogame{Title = "Splinter Cell Conviction", Year = 2010, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "You are a rogue agent, unmask the corrupted boss of Third Echelon while you discover the truth about you daughter",
                    Cover = "https://static-it.gamestop.it/images/products/145573/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/i6E2D11n-qQ"},
                new Videogame{Title = "Splinter Cell Blacklist", Year = 2013, Genre = "Stealth",
                    SoftwareHouse = "Ubisoft", Publisher = "Ubisoft", Synopsis = "Sam Fisher is now again a member of NSA, and this time he will be the boss of Fourth Echelon, answering only to the President",
                    Cover = "https://static.wikia.nocookie.net/splintercell/images/e/e8/Splinter_Cell_Blacklist_Cover.jpg",
                    Trailer = "https://www.youtube.com/embed/wYGoFH6bWXg"},
                new Videogame{Title = "Halo Infinite", Year = 2021, Genre = "FPS",
                    SoftwareHouse = "343 Industries", Publisher = "Microsoft Game Studios", Synopsis = "Be again Master Chief, and save the galaxy from the evil of the Covenant",
                    Cover = "https://static-it.gamestop.it/images/products/305865/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/PyMlV5_HRWk"},
                new Videogame{Title = "Call of Duty Modern Warfare", Year = 2019, Genre = "FPS",
                    SoftwareHouse = "Infinity Ward", Publisher = "Activision", Synopsis = "Complete an important mission to stop the russians",
                    Cover = "https://static-it.gamestop.it/images/products/294129/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/bH1lHCirCGI"},
                new Videogame{Title = "Bloodborne", Year = 2015, Genre = "GDR",
                    SoftwareHouse = "From Software", Publisher = "Sony Entertainment", Synopsis = "Explore a world of crazyness and kill the bosses",
                    Cover = "https://static-it.gamestop.it/images/products/269355/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/2Crk_GpxGQE"},
                new Videogame{Title = "GTA V", Year = 2013, Genre = "Open World",
                    SoftwareHouse = "Rockstar Games", Publisher = "Take Two", Synopsis = "Live the lifes of three criminals of Los Santos: Mike, Franklin and Trevor",
                    Cover = "https://static-it.gamestop.it/images/products/304447/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/hvoD7ehZPcM"},
                new Videogame{Title = "Red Dead Redemption 2", Year = 2018, Genre = "Open World",
                    SoftwareHouse = "Rockstar Games", Publisher = "Take Two", Synopsis = "You are Arthur Morgan, of the last cowboys of the western age",
                    Cover = "https://static-it.gamestop.it/images/products/275326/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/HVRzx17WHVk"},
                new Videogame{Title = "Mass Effect Legendary Edition", Year = 2020, Genre = "GDR",
                    SoftwareHouse = "Bioware", Publisher = "EA", Synopsis = "Play three masterpicies that created the famous series of Mass Effect",
                    Cover = "https://static-it.gamestop.it/images/products/302639/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/n8i53TtQ6IQ"},
                new Videogame{Title = "Until Dawn", Year = 2015, Genre = "Horror",
                    SoftwareHouse = "Supermassive Games", Publisher = "Sony Entertainment", Synopsis = "It's winter, and a group of friends reunite in a chalet on the mountains...what will happen?",
                    Cover = "https://static-it.gamestop.it/images/products/260536/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/3NcF7EOnjow"},
                new Videogame{Title = "The Quarry", Year = 2022, Genre = "Horror",
                    SoftwareHouse = "Supermassive Games", Publisher = "Take Two", Synopsis = "A group of nine friends go to camping together...",
                    Cover = "https://static-it.gamestop.it/images/products/307911/3max.jpg",
                    Trailer = "https://www.youtube.com/embed/Zh2K7SxRHmo"},
            };
            var users = new User[]
        {
                new User{Email = "albertolucchesi97@gmail.com", Admin = false,
                    AuthId = "google-oauth2|112923060526370503250",
                },
                new User { Email = "test@test.com", Admin = true,
                    AuthId = "auth0|62837df25bbfac0067171efe" }
    };
            var videogamesOwned = new VideogameOwned[]
           {
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 1},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 2},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 3},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 4},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 5},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 6},
                new VideogameOwned{UserEmail = "test@test.com", VideogameId = 7},
           };
            var videogamesWishlist = new VideogameWishlist[]
            {
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 10},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 11},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 12},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 13},
                new VideogameWishlist{UserEmail = "test@test.com", VideogameId = 14},

            };
     

            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();
            
            if(!context.Videogames.Any())
            {
                System.Console.WriteLine("Adding videogames data - seeding...");
                context.Videogames.AddRange(videogames);
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have videogames data - not seeding");
            }

            if (!context.Users.Any())
            {
                System.Console.WriteLine("Adding users data - seeding...");
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have users data - not seeding");
            }

            if (!context.VideogamesOwned.Any())
            {
                System.Console.WriteLine("Adding videogames owned data - seeding...");
                context.VideogamesOwned.AddRange(videogamesOwned);
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have videogames owned data - not seeding");
            }

            if (!context.VideogamesWishlist.Any())
            {
                System.Console.WriteLine("Adding videogames wishlist data - seeding...");
                context.VideogamesWishlist.AddRange(videogamesWishlist);
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have videogames wishlist data - not seeding");
            }
        }
            
        //public static void Initialize(JustPlayContext context)
        //{
        //    context.Database.EnsureCreated();

        //    if (context.Videogames.Any())
        //    {
        //        return;
        //    }

            
        

        //    foreach (Videogame videogame in videogames)
        //    {
        //        context.Videogames.Add(videogame);
        //    }
        //    context.SaveChanges();

        //    foreach (User user in users)
        //    {
        //        context.Users.Add(user);
        //    }
        //    context.SaveChanges();

        //    foreach (VideogameOwned videogameOwned in videogamesOwned)
        //    {
        //        context.VideogamesOwned.Add(videogameOwned);
        //    }
        //    context.SaveChanges();

        //    foreach (VideogameWishlist videogameWishlist in videogamesWishlist)
        //    {
        //        context.VideogamesWishlist.Add(videogameWishlist);
        //    }

        //    context.SaveChanges();
        //}
    }
}
