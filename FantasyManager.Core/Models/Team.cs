using System.Collections.Generic;

namespace FantasyManager.Core.Models
{
    public class Team
    {
        #region Properties

        public long Id { get; set; }

        public string Name { get; set; }

        public byte[] Logo { get; set; }

        #endregion

        #region Relationships

        public virtual ICollection<Player> Roster { get; set; }

        #endregion
    }

    public class FantasyTeam : Team { }
}
