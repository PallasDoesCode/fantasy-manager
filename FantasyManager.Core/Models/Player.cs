using FantasyManager.Core.Extensions;
using System;
using System.ComponentModel;

namespace FantasyManager.Core.Models
{
    public enum PlayerPosition
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

        private PlayerPosition _position;

        #region Properties

        public long Id { get; set; }

        public string Name { get; set; }

        public byte[] Avatar { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Status { get; set; }

        public string Position
        {
            get
            {
                return _position.GetDescription();
            }
            set
            {
                switch( value )
                {
                    case "Quarterback":
                        _position = PlayerPosition.Quarterback;
                        break;
                    case "Running Back":
                        _position = PlayerPosition.RunningBack;
                        break;
                    case "Wide Receiver":
                        _position = PlayerPosition.WideReceiver;
                        break;
                    case "Tight End":
                        _position = PlayerPosition.TightEnd;
                        break;
                    case "FLEX":
                        _position = PlayerPosition.FLEX;
                        break;
                    case "Defense and Special Teams":
                        _position = PlayerPosition.DST;
                        break;
                    case "Kicker":
                        _position = PlayerPosition.Kicker;
                        break;
                }
            }
        }

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
