using FBData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Interface
{
    public interface IPlayerData
    {
        void Add(Player player);

        IEnumerable<Player> GetAll();

        Player GetPlayer(int id);

        void DeletePlayer(int id);

        void UpdatePlayer(Player player);
    }
}
