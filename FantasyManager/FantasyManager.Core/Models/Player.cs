using System;
using System.ComponentModel;

namespace FantasyManager.Core.Models
{
    public enum Position
    {
        [Description( "Quarterback" )]
        Quarterback,

        [Description( "Running Back" )]
        RunningBack,

        [Description( "Wide Receiver" )]
        WideReceiver,

        [Description( "Tight End" )]
        TightEnd,

        [Description( "FLEX" )]
        FLEX,

        [Description( "Defense and Special Teams" )]
        DST,

        [Description( "Kicker" )]
        Kicker
    }

    /// <summary>
    /// Represents the Player (i.e. Julio Jones)
    /// </summary>
    public class Player
    {
        #region Properties

        public long Id { get; set; }

        public string Name { get; set; }

        public byte[] Avatar { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Status { get; set; }

        public Position Position { get; set; }

        // in inches
        public int Height { get; set; }

        public int Weight { get; set; }

        public long? TeamId { get; set; }

        public long? StatsId { get; set; }

        #endregion

        #region Relationships

        public virtual Team Team { get; set; }

        public virtual Statistics Statistics { get; set; }

        #endregion
    }
}
