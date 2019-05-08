using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public Player GetPlayerById( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            var player = _repository.GetByIdAsync<Player>( id ).Result;
            player.Statistics = GetStatisticsByPlayer( player.StatsId.Value ).Result;

            return player;
        }

        public IEnumerable<Player> ListPlayers()
        {
            var players = _repository.ListAsync<Player>().Result;
            foreach( var player in players )
            {
                player.Statistics = GetStatisticsByPlayer( player.StatsId.Value ).Result;
            }

            return players;
        }

        public async Task<IEnumerable<Player>> ListPlayersByTeamId( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return await _repository.ListAsync<Player>( o => o.TeamId == id );
        }

        public async Task<Statistics> GetStatisticsByPlayer( long id )
        {
            if ( id <= 0 )
                throw new ArgumentException( "id must be greater than 0" );

            return await _repository.GetByIdAsync<Statistics>( id );
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
            //var stats = await _repository.GetByIdAsync<Statistics>( player.StatsId.Value );

            // Player has to be deleted first since is has a FK to the Statistics table
            var playerResult = await _repository.DeleteAsync( player );

            // TODO: I ran into an issue deleting the statistics record.
            // In the future I need to investigate why this fails and fix it.
            //var statsResult = await _repository.DeleteAsync( stats );

            return playerResult;
        }

        #endregion
    }
}
