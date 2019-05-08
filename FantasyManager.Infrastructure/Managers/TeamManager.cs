using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FantasyManager.Core.Models;
using FantasyManager.Infrastructure.Interfaces;
using FantasyManager.Infrastructure.Repository;

namespace FantasyManager.Infrastructure.Managers
{
    public class TeamManager : ITeamManager
    {
        #region Fields

        private IDbRepository _repository = null;

        #endregion

        #region Constructor

        public TeamManager( IDbRepository repository )
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<Team> GetTeamById( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return await _repository.GetByIdAsync<Team>( id );
        }

        public IEnumerable<Team> ListTeams()
        {
            var teams = _repository.ListAsync<Team>().Result;

            foreach (var team in teams)
            {
                team.Roster = GetTeamRoster( team.Id ).ToList<Player>();
            }

            return teams;
        }

        public async Task<int> AddTeam( Team team )
        {
            return await _repository.CreateAsync( team );
        }

        public async Task<int> UpdateTeam( Team team )
        {
            return await _repository.UpdateAsync( team );
        }

        public async Task<int> DeleteTeam( Team team )
        {
            return await _repository.DeleteAsync( team );
        }

        #endregion

        private IQueryable<Player> GetTeamRoster( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return _repository.Get<Player>( o => o.TeamId == id );
        }
    }
}
