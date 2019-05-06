using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Team>> ListTeams()
        {
            var data = await _repository.ListAsync<Team>();
            return data;
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
    }
}
