using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustPlay.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IDataRepository _dataRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IDataRepository repository, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dataRepository = repository;
            _httpContextAccessor = httpContextAccessor;

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserByEmail()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = authorization.ToString().Split(' ')[1];
            
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;

            var user = await _dataRepository.GetUserByEmail(userEmail);
            if (user != null)
            {
                return user;
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("videogamesOwned")]
        public async Task<IEnumerable<Videogame>> GetUserVideogamesOwned()
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                return await _dataRepository.GetVideogamesOwned(userEmail);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize]
        [HttpGet("videogamesWishlist")]
        public async Task<IEnumerable<Videogame>> GetUserVideogamesWishlist()
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                return await _dataRepository.GetVideogamesWishlist(userEmail);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize]
        [HttpPost("videogamesWishlist/add/{videogameId}")]
        public async Task<int> AddVideogamesToWishlist(int videogameId)
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                _dataRepository.AddVideogameToWishlist(userEmail, videogameId);
                return videogameId;
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("videogamesWishlist/remove/{videogameId}")]
        public async Task<int> RemoveVideogamesFromWishlist(int videogameId)
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                _dataRepository.RemoveVideogameFromWishlist(userEmail, videogameId);
                return videogameId;
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize]
        [HttpPost("videogamesOwned/add/{videogameId}")]
        public async Task<int> AddVideogamesToOwned(int videogameId)
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                _dataRepository.AddVideogameToOwned(userEmail, videogameId);
                return videogameId;
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("videogamesOwned/remove/{videogameId}")]
        public async Task<int> RemoveVideogamesFromOwned(int videogameId)
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;
                _dataRepository.RemoveVideogameFromOwned(userEmail, videogameId);
                return videogameId;
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                if (user != null)
                {
                    return await _dataRepository.PostUser(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has been catched: {0}", e);
                throw;
            }
        }
    }
}
