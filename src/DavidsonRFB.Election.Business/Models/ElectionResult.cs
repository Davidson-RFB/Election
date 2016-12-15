using System.Collections.Generic;

namespace DavidsonRFB.Election.Business.Models
{
    public class ElectionResult
    {
        public int PositionId { get; set; }
        public int SuccessfulNomineeId { get; set; }
        public IList<ElectionRoundResult> RoundResults { get; set; }
    }

    public class ElectionRoundResult
    {
        public int Round { get; set; }
        public int TotalVotes { get; set; }
        public int InvalidVotes { get; set; }
        public IList<ElectionRoundNomineeResult> NomineeResults { get; set; }
    }

    public class ElectionRoundNomineeResult
    {
        public int NomineeId { get; set; }
        public int Votes { get; set; }
    }
}
