using System.Collections.Generic;

namespace FantasyManager.Core.Models
{

    /// <summary>
    /// Represents the logged in user
    /// </summary>
    public class User
    {
        #region Properties

        /// <summary>
        /// Unique identifer representing the user
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's e-mail
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// A user can participate in multiple Fantasy Football leagues; therefore, they can create multiple teams.
        /// </summary>
        public virtual ICollection<FantasyTeam> Teams { get; set; }

        #endregion
    }
}
