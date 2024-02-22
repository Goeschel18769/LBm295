using LBm295.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LBm295.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly Game _game;
        private readonly PublisherDB _publisherDB;

        public GameController(PublisherDB publisherDB)
        {
            _publisherDB = publisherDB;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            if (_publisherDB.Publishers == null)
            {
                return NotFound();
            }
            return await _publisherDB.Games.Select(x => GameToGameDto(x)).ToListAsync();
        }

        //Get function for one specific game
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(Guid id)
        {
            if (_publisherDB.Games == null)
            {
                return NotFound();
            }
            var games = await _publisherDB.Games.FindAsync(id);

            if (games == null)
            {
                return NotFound();
            }

            return GameToGameDto(games);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, GameDto gameDto)
        {
            if (id != gameDto.Id)
            {
                return BadRequest();
            }
            var mitarbeiter = await _publisherDB.Publishers.FindAsync(gameDto.Id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }
            gameDto.GameName = gameDto.GameName;
            gameDto.Revenue = gameDto.Revenue;
            gameDto.ReleaseDate = gameDto.ReleaseDate;

            _publisherDB.Entry(mitarbeiter).State = EntityState.Modified;

            try
            {
                await _publisherDB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost("addGame")]
        public ActionResult<Game> AddGame(GameDto request)
        {
            var newGame = new Game
            {
                Id = request.Id,
                GameName = request.GameName,
                Revenue = request.Revenue,
                ReleaseDate = request.ReleaseDate
            };



            return Ok(newGame);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            if (_publisherDB.Games == null)
            {
                return NotFound();
            }
            var game = await _publisherDB.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _publisherDB.Games.Remove(game);
            await _publisherDB.SaveChangesAsync();

            return NoContent();
        }

        private static GameDto GameToGameDto(Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                GameName = game.GameName,
                Revenue = game.Revenue,
                ReleaseDate = game.ReleaseDate

            };

        }
        private bool GameExists(Guid id)
        {
            return (_publisherDB.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        

            
        
    }
}
