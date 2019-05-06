using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FantasyManager.Core.Models;
using FantasyManager.Infrastructure.Interfaces;
using FantasyManager.Infrastructure.Repository;

namespace FantasyManager.Infrastructure.Managers
{
    public class PlayerManager : IPlayerManager
    {
        #region Fields

        private IDbRepository _repository = null;

        #endregion

        #region Constructor

        public PlayerManager( IDbRepository repository )
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<Player> GetPlayerById( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return await _repository.GetByIdAsync<Player>( id );
        }

        public async Task<IEnumerable<Player>> ListPlayers()
        {
            return await _repository.ListAsync<Player>();
        }

        public async Task<int> AddPlayer( Player player )
        {
            return await _repository.CreateAsync( player );
        }

        public async Task<int> UpdatePlayer( Player player )
        {
            return await _repository.UpdateAsync( player );
        }

        public async Task<int> DeletePlayer( Player player )
        {
            return await _repository.DeleteAsync( player );
        }

        #endregion
    }
}
