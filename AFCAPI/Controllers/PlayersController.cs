using FBData.Context;
using FBData.DataHandler;
using FBData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using FBData.Interface;
using FBData.Helper;
using System.Text.Json;

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
            var data = new PlayerData(_context);
            return data.GetAll().ToList();
        }

        // GET: api/Players1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var data = new PlayerData(_context);
            var player = data.GetPlayer(id);
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
            try
            {
                if (id != player.PlayerId)
                {
                    return BadRequest();
                }
                var data = new PlayerData(_context);
                data.UpdatePlayer(player);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, JsonSerializer.Serialize( ex.Message));
            }
            catch (HandledException ex)
            {
                return StatusCode(500, JsonSerializer.Serialize( ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, JsonSerializer.Serialize ("Internal Server Error, Please try again or if this persists contact support"));
                //Logging would record any unhandled exception detail (with stack) and keep any important and unnecessay info from the client side. If all the clientside validation worked this should be the only thing that comes out of Error
            }

            return Ok(await _context.Players.ToListAsync()); ;
        }

        // POST: api/Players1
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            try
            {
                if (player == null)
                {
                    return BadRequest();
                }
                var data = new PlayerData(_context);
                data.Add(player);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, JsonSerializer.Serialize(ex.Message));
            }
            catch (HandledException ex)
            {
                return StatusCode(500, JsonSerializer.Serialize(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, JsonSerializer.Serialize("Internal Server Error, Please try again or if this persists contact support"));
                //Logging would record any unhandled exception detail (with stack) and keep any important and unnecessay info from the client side
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
            try
            {
                var data = new PlayerData(_context);
                data.DeletePlayer(id);

            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (HandledException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error, Please try again or if this persists contact support");
                //Logging would record any unhandled exception detail (with stack) and keep any important and unnecessay info from the client side
            }

            return Ok(await _context.Players.ToListAsync());
        }



    }
}
