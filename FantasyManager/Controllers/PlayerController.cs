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
        public async Task<ActionResult> ListAllPlayers()
        {
            var result = await _playerManager.ListPlayers();
            return Ok( result );
        }


        [HttpGet( "{id}" )]
        public ActionResult GetPlayerById( long id )
        {
            var result = _playerManager.GetPlayerById( id );
            return Ok( result );
        }

        [HttpPost]
        public ActionResult CreatePlayer( [FromBody] Player player )
        {
            var result = _playerManager.AddPlayer( player );
            return Ok( result );
        }

        [HttpPost]
        public ActionResult UpdatePlayer( [FromBody] Player player )
        {
            var result = _playerManager.UpdatePlayer( player );
            return Ok( result );
        }

        [HttpPost]
        public ActionResult DeletePlayer( [FromBody] Player player )
        {
            var result = _playerManager.DeletePlayer( player );
            return Ok( result );
        }
    }
}
