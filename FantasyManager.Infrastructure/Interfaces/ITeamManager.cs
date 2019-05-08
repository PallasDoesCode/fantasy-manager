using System.Collections.Generic;
using System.Threading.Tasks;

using FantasyManager.Core.Models;

namespace FantasyManager.Infrastructure.Interfaces
{
    public interface ITeamManager
    {
        Task<int> AddTeam( Team team );

        Task<int> DeleteTeam( Team team );

        Task<Team> GetTeamById( long id );

        IEnumerable<Team> ListTeams();

        Task<int> UpdateTeam( Team team );
    }
}