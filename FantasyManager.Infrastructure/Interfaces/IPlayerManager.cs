using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyManager.Core.Models;

namespace FantasyManager.Infrastructure.Interfaces
{
    public interface IPlayerManager
    {
        Task<int> AddPlayer( Player player );

        Task<int> DeletePlayer( Player player );

        Player GetPlayerById( long id );

        Task<Statistics> GetStatisticsByPlayer( long id );

        IEnumerable<Player> ListPlayers();

        Task<IEnumerable<Player>> ListPlayersByTeamId( long id );

        Task<int> UpdatePlayer( Player player );
    }
}