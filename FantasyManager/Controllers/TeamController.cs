using Microsoft.AspNetCore.Mvc;

using FantasyManager.Core.Models;
using FantasyManager.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace FantasyManager.Web.Controllers
{
    [Route("api/team")]
    public class TeamController : Controller
    {
        ITeamManager _teamManager = null;

        public TeamController( ITeamManager teamManager)
        {
            _teamManager = teamManager;
        }

        [HttpGet("list")]
        public ActionResult ListAllTeams()
        {
            var result = _teamManager.ListTeams();
            return Ok(result);
        }

        [HttpGet( "{id}" )]
        public ActionResult GetTeamById( long id )
        {
            var result = _teamManager.GetTeamById( id );
            return Ok( result );
        }

        [HttpPost( "create" )]
        public ActionResult CreateTeam( [FromBody] Team team )
        {
            var result = _teamManager.AddTeam( team );
            return Ok( result );
        }

        [HttpPost( "update" )]
        public ActionResult UpdateTeam( [FromBody] Team team )
        {
            var result = _teamManager.UpdateTeam( team );
            return Ok( result );
        }

        [HttpPost( "delete" )]
        public ActionResult DeleteTeam( [FromBody] Team team )
        {
            var result = _teamManager.DeleteTeam( team );
            return Ok( result );
        }
    }
}
