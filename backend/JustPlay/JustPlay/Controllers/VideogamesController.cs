using JustPlay.Data.Models;
using JustPlay.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustPlay.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class VideogamesController : Controller
    {
        private readonly IDataRepository _repository;

        public VideogamesController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Videogame>> GetAllVideogames(string? search, string? sort)
        {

            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    if (string.IsNullOrEmpty(sort))
                        return await _repository.GetAllVideogames(null, null);
                    else
                        return await _repository.GetAllVideogames(null, sort);

                }
                else
                {
                    if (string.IsNullOrEmpty(sort))
                        return await _repository.GetAllVideogames(search, null);
                    else
                        return await _repository.GetAllVideogames(search, sort);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has been catched: {0}", e);
                throw;
            }  
        }

        [HttpGet("{videogameId}")]
        public async Task<ActionResult<Videogame>> GetVideogame(int videogameId)
        {
            try
            {
                var videogame = await _repository.GetVideogame(videogameId);

                if (videogame != null)
                {
                    return videogame;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPost]
        public async Task<ActionResult<Videogame>> PostVideogame(Videogame videogame)
        {
            try
            {
                if (videogame != null)
                {
                    
                    await _repository.PostVideogame(videogame);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has been catched: {0}", e);
                throw;
            }

            return CreatedAtAction(nameof(GetVideogame), new { videogameId = videogame.ID }, videogame);
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPut("{videogameId}")]
        public async Task<ActionResult<Videogame>> PutVideogame(int videogameId,Videogame vgUpdated)
        {
            try
            {
                var vgToUpdate = await _repository.GetVideogame(videogameId);

                if (vgToUpdate != null && vgToUpdate != null)
                {
                    var updatedVideogame = await _repository.PutVideogame(vgUpdated, vgToUpdate);
                    return updatedVideogame;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has been catched: {0}", e);
                throw;
            }
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpDelete("{videogameId}")]
        public async Task<ActionResult> DeleteVideogame(int videogameId)
        {       
            try
            {
                var vg = await _repository.GetVideogame(videogameId);

                if (vg != null)
                {
                    _repository.DeleteVideogame(vg);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has been catched: {0}", e);
                throw;
            } 
        }
    }
}
