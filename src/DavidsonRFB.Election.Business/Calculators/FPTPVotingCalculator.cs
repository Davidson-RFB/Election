using System.Linq;
using System.Collections.Generic;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;

namespace DavidsonRFB.Election.Business.Calculators
{
    public class FPTPVotingCalculator : IVotingCalculator
    {
        private IElectionRepository _repository;

        public FPTPVotingCalculator(IElectionRepository repository) : base()
        {
            _repository = repository;
        }

        public ElectionResult CalculateElectionResult(int positionId)
        {
            Position position = _repository.GetPosition(positionId);

            // Set up the Nominees for the one and only round of voting
            List<ElectionRoundNomineeResult> nomineeResults = new List<ElectionRoundNomineeResult>();
            foreach (Nominee nominee in position.Nominees)
            {
                nomineeResults.Add(new ElectionRoundNomineeResult()
                {
                    NomineeId = nominee.Id,
                    Votes = position.Votes.Count(v => v.NomineeId == nominee.Id &&
                                                     !v.HasAbstained &&
                                                      v.PreferenceOrder == 1)
                });
            }

            // Create the one and only round result
            ElectionRoundResult roundResult = new ElectionRoundResult()
            {
                NomineeResults = nomineeResults,
                Round = 1,
                TotalVotes = nomineeResults.Sum(n => n.Votes),
            };

            // ... and the generic result object
            ElectionResult result = new ElectionResult()
            {
                PositionId = positionId,
                SuccessfulNomineeId = nomineeResults.Aggregate((n1, n2) => n1.Votes > n2.Votes ? n1 : n2).NomineeId,
                RoundResults = new List<ElectionRoundResult>()
            };
            result.RoundResults.Add(roundResult);

            return result;
        }
    }
}
