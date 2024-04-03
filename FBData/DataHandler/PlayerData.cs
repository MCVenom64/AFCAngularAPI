using FBData.Context;
using FBData.Helper;
using FBData.Interface;
using FBData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FBData.DataHandler
{
    public class PlayerData : IPlayerData
    {
        private readonly FBContext _context;
        private readonly PlayerDataHelper _helper;
        public PlayerData(FBContext fBContext)
        {
            _context = fBContext;
            _helper = new PlayerDataHelper(_context);
        }
        public void Add(Player player)
        {
            _context.Players.Add(player);
            _helper.Validate(player);
            _context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);

            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
            else
            {
                throw new HandledException("Delete Failed, User Not Found");
            }
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Players;
        }

        public Player GetPlayer(int id)
        {
            var player = _context.Players.Where(x => x.PlayerId == id).FirstOrDefault();

            if(player == null)
            {
                throw new HandledException("Player Not found");
            }

            return player;
        }

        public void UpdatePlayer(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            _helper.Validate(player);
            _context.SaveChanges();

        }
    }
}
