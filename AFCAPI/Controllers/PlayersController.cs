using FBData.Context;
using FBData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly FBContext _context;

        public PlayersController(FBContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        // PUT: api/Players1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                if (this.IsJerseyNotUnique(player))
                {
                    return StatusCode(500, "Jersey Number is not Unique");
                }
                else if (this.IsPlayerNotUnique(player))
                {
                    return StatusCode(500, "Player Name is not Unique");
                }
                else
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return  Ok(await _context.Players.ToListAsync()); ;
        }

        // POST: api/Players1
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {

            if (this.IsJerseyNotUnique(player))
            {
                return StatusCode(500, "Jersey Number is not Unique");
            }else if(this.IsPlayerNotUnique(player))
            {
                return StatusCode(500, "Player Name is not Unique");
            }
            else
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();
            }
            return Ok(await _context.Players.ToListAsync());
        }

        // DELETE: api/Players1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(await _context.Players.ToListAsync()); 
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId  == id);
        }

        private bool IsJerseyNotUnique(Player player)
        {
            return _context.Players.Any(e=> e.JerseyNumber == player.JerseyNumber && player.PlayerId != player.PlayerId );
        }

        private bool IsPlayerNotUnique(Player player)
        {if (player.PlayerName != null)
            {
                return _context.Players.Any(e => e.PlayerName.Equals(player.PlayerName, StringComparison.CurrentCultureIgnoreCase) && player.PlayerId != player.PlayerId);
            }
        }

    }
}
