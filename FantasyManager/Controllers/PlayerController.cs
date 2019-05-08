using Microsoft.AspNetCore.Mvc;

using FantasyManager.Core.Models;
using FantasyManager.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace FantasyManager.Web.Controllers
{
    [Route("api/player")]
    public class PlayerController : Controller
    {
        IPlayerManager _playerManager = null;
        public PlayerController( IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        [HttpGet( "list" )]
        public ActionResult ListAllPlayers()
        {
            var result = _playerManager.ListPlayers();
            return Ok( result );
        }

        [HttpGet( "list/{teamId}" )]
        public async Task<ActionResult> ListAllPlayers( long teamId )
        {
            var result = await _playerManager.ListPlayersByTeamId( teamId );
            return Ok( result );
        }

        [HttpGet( "{id}" )]
        public ActionResult GetPlayerById( long id )
        {
            var result = _playerManager.GetPlayerById( id );
            return Ok( result );
        }

        [HttpPost( "create" )]
        public ActionResult CreatePlayer( [FromBody] Player player )
        {
            var result = _playerManager.AddPlayer( player ).Result;
            return Ok( result );
        }

        [HttpPost( "update" )]
        public ActionResult UpdatePlayer( [FromBody] Player player )
        {
            var result = _playerManager.UpdatePlayer( player );
            return Ok( result );
        }

        [HttpPost( "delete" )]
        public ActionResult DeletePlayer( [FromBody] Player player )
        {
            var result = _playerManager.DeletePlayer( player );
            return Ok( result );
        }
    }
}
