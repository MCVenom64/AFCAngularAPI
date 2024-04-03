using FBData.Context;
using FBData.Interface;
using FBData.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Helper
{
    public class PlayerDataHelper
    {
       private readonly FBContext _context;
        public PlayerDataHelper(FBContext context)
        {
            _context = context;
        }

        private  bool IsJerseyNotUnique(Player player)
        {
            return _context.Players.Any(e => e.JerseyNumber == player.JerseyNumber && e.PlayerId != player.PlayerId);
        }

        private bool IsPlayerNotUnique(Player player)
        {
            if (player.PlayerName != null)
            {
                return _context.Players.Any(e => e.PlayerName.ToLower() == player.PlayerName.ToLower() && e.PlayerId != player.PlayerId);
            }
            return true;
        }

        public void Validate(Player player)
        {
            if (this.IsJerseyNotUnique(player))
            {
                throw new ValidationException("Jersey is not unique");
            }
            else if (this.IsPlayerNotUnique(player))
            {
                throw new ValidationException("Player Name is not Unique");
            }
        }
    }
}
