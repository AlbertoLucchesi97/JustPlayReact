using JustPlay.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustPlay.Data.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly JustPlayContext _context;

        public DataRepository(JustPlayContext context)
        {
            _context = context;
        }

        #region Videogames
        public void DeleteVideogame(Videogame vg)
        {
            _context.Videogames.Remove(vg);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Videogame>> GetAllVideogames(string? search, 
            string? filter)
        {
            if (string.IsNullOrEmpty(search)) 
            {
                if (filter == "year")
                    return await _context.Videogames.OrderBy(v => v.Year).ToListAsync();
                else if (filter == "title" || string.IsNullOrEmpty(filter))
                    return await _context.Videogames.OrderBy(v => v.Title).ToListAsync();
                else
                    return await _context.Videogames.Where(v => v.Genre.Contains(filter)).ToListAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(filter))
                {
                    return await _context.Videogames.Where(vg => vg.Title.Contains(search))
                        .ToListAsync();
                }
                else
                {
                    if (filter == "year")
                        return await _context.Videogames.Where(vg => vg.Title.Contains(search))
                        .OrderBy(v => v.Year).ToListAsync();
                    else
                        return await _context.Videogames.Where(vg => vg.Title.Contains(search))
                            .OrderBy(v => v.Title).ToListAsync();
                }
            }
        }

        public async Task<Videogame> GetVideogame(int videogameId)
        {
            var videogame = await _context.Videogames.FindAsync(videogameId);
            return videogame;
        }

        public async Task<Videogame> PostVideogame(Videogame videogame)
        {
            if (videogame != null)
            {
                await _context.Videogames.AddAsync(videogame);
                _context.SaveChanges();
            }
            return await GetVideogame(videogame.ID);
        }

        public async Task<Videogame> PutVideogame(Videogame vgUpdated, Videogame vgToUpdate)
        {
                vgToUpdate.Title = vgUpdated.Title;
                vgToUpdate.Year = vgUpdated.Year;
                vgToUpdate.Genre = vgUpdated.Genre;
                vgToUpdate.SoftwareHouse = vgUpdated.SoftwareHouse;
                vgToUpdate.Publisher = vgUpdated.Publisher;
                vgToUpdate.Synopsis = vgUpdated.Synopsis;
                vgToUpdate.Cover = vgUpdated.Cover;
                vgToUpdate.Trailer = vgUpdated.Trailer;

                _context.Videogames.Update(vgToUpdate);
                _context.SaveChanges();

                return await GetVideogame(vgToUpdate.ID);
        }
        #endregion

        #region Users
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<Videogame>> GetVideogamesOwned(string? email)
        {
            if (email == null)
                return null;
            
            var videogamesIdList = await _context.VideogamesOwned.Where(vg => vg.UserEmail == email).Select(vg => vg.VideogameId).ToListAsync();
            var videogames = await _context.Videogames.Where(vg => videogamesIdList.Contains(vg.ID)).ToListAsync();
            return videogames;
        }

        public async Task<IEnumerable<Videogame>> GetVideogamesWishlist(string? email)
        {
            if (email == null)
                return null;
            
            var videogamesIdList = await _context.VideogamesWishlist.Where(vg => vg.UserEmail == email).Select(vg => vg.VideogameId).ToListAsync();
            var videogames = await _context.Videogames.Where(vg => videogamesIdList.Contains(vg.ID)).ToListAsync();
            return videogames;
        }

        public async Task<User> PostUser(User user)
        {
            if (user != null)
            {
                await _context.Users.AddAsync(user);
                _context.SaveChanges();
            }
            return await GetUserByEmail(user.Email);
        }

        public async void AddVideogameToWishlist(string email, int videogameId)
        {
            if (email != null && videogameId != 0)
            {
                var videogameWishlist = new VideogameWishlist
                {
                    UserEmail = email,
                    VideogameId = videogameId
                };
                await _context.VideogamesWishlist.AddAsync(videogameWishlist);
                _context.SaveChanges();
            }
        }

        public void RemoveVideogameFromWishlist(string email, int videogameId)
        {
            if (email != null && videogameId != 0)
            {
                var videogameWishlist = _context.VideogamesWishlist.Where(vg => vg.UserEmail == email && vg.VideogameId == videogameId).FirstOrDefault();
                _context.VideogamesWishlist.Remove(videogameWishlist);
                _context.SaveChanges();
            }
        }

        public async void AddVideogameToOwned(string email, int videogameId)
        {
            if (email != null && videogameId != 0)
            {
                var videogameOwned = new VideogameOwned
                {
                    UserEmail = email,
                    VideogameId = videogameId
                };
                await _context.VideogamesOwned.AddAsync(videogameOwned);
                _context.SaveChanges();
            }
        }

        public void RemoveVideogameFromOwned(string email, int videogameId)
        {
            if (email != null && videogameId != 0)
            {
                var videogameOwned = _context.VideogamesOwned.Where(vg => vg.UserEmail == email && vg.VideogameId == videogameId).FirstOrDefault();
                _context.VideogamesOwned.Remove(videogameOwned);
                _context.SaveChanges();
            }
        }
        #endregion
    }
}
