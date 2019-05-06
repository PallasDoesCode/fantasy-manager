using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyManager.Core.Models;

namespace FantasyManager.Infrastructure.Interfaces
{
    public interface IPlayerManager
    {
        Task<int> AddPlayer( Player player );

        Task<int> DeletePlayer( Player player );

        Task<Player> GetPlayerById( long id );

        Task<IEnumerable<Player>> ListPlayers();

        Task<int> UpdatePlayer( Player player );
    }
}