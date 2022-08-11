using JustPlay.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustPlay.Data.Repository
{
    public interface IDataRepository
    {
        Task<IEnumerable<Videogame>> GetAllVideogames(string? search, string? filter);
        Task<Videogame> GetVideogame(int videogameId);
        Task<Videogame> PostVideogame(Videogame videogame);
        Task<Videogame>PutVideogame(Videogame vgUpdated, Videogame vgToUpdate);
        void DeleteVideogame(Videogame vg);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<Videogame>> GetVideogamesOwned(string? email);
        Task<IEnumerable<Videogame>> GetVideogamesWishlist(string? email);
        void AddVideogameToWishlist(string email, int videogameId);
        void RemoveVideogameFromWishlist(string email, int videogameId);
        void AddVideogameToOwned(string email, int videogameId);
        void RemoveVideogameFromOwned(string email, int videogameId);
        Task<User> PostUser(User user);
    }
}
