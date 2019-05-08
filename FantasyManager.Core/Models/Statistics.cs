using System;

namespace FantasyManager.Core.Models
{
    public class Statistics
    {
        #region Properties

        public long Id { get; set; }

        public long Season { get; set; } // i.e. 2016, 2017, 2018, etc.

        #region Receiving

        public int? GamesPlayed { get; set; } // GP

        public int? Receptions { get; set; } // REC

        public int? ReceivingTargets { get; set; } // TGTS

        public int? ReceivingYards { get; set; } // YDS

        public double? YardsPerReception { get; set; } // AVG

        public int? ReceivingTouchdowns { get; set; } // TD

        public int? LongReception { get; set; } // LNG

        public int? ReceivingFirstDowns { get; set; } // FD

        public int? ReceivingFumbles { get; set; } // FUM

        public int? ReceivingFumblesLost { get; set; } // LST

        #endregion

        #region Rushing

        public int? RushingAttempts { get; set; } // ATT

        public int? RushingYards { get; set; } // YDS

        public int? YardsPerRushAttempt { get; set; } // AVG

        public int? RushingTouchdowns { get; set; } // TD

        public int? LongRushing { get; set; } // LNG

        public int? RushingFirstDowns { get; set; } // FD

        public int? RushingFumbles { get; set; } // FUM

        public int? RushingFumblesLost { get; set; } // LST

        #endregion

        #region Defensive

        public int? TotalTackles { get; set; } // TOT

        public int? SoloTackles { get; set; } // SOLO

        public int? AssistTackles { get; set; } // AST

        public int? Sacks { get; set; } // SACK

        public int? ForcedFumbles { get; set; } // FF

        public int? FumblesRecovered { get; set; } // FR

        public int? FumblesRecoveredYards { get; set; } // YDS

        public int? Interceptions { get; set; } // INT

        public int? InterceptionYards { get; set; } // YDS

        public int? AverageInterceptionYards { get; set; } // AVG

        public int? InterceptionTouchdowns { get; set; } // TD

        public int? LongInterception { get; set; } // LNG

        public int? PassesDefended { get; set; } // PD

        public int? Stuffs { get; set; } // STf ( Stuff for Tackles for negative yards )

        public int? YardsLost { get; set; } // STFYDS or Stuff Yards

        public int? KicksBlocked { get; set; } // KB

        #endregion

        #region Scoring

        public int? PassingTouchdowns { get; set; } // PASS

        public int? ReturnTouchdowns { get; set; } // RET

        public int? TotalTouchdowns { get; set; } // TD

        public int? TotalTwoPointConversions { get; set; } // 2PT

        public int? KickExtraPoints { get; set; } // PAT

        public int? FieldGoals { get; set; } // FG

        public int? TotalPoints { get; set; } // PTS

        #endregion

        #endregion
    }
}
