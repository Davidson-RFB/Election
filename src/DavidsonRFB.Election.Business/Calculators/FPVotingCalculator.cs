using System.Linq;
using System.Collections.Generic;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;

namespace DavidsonRFB.Election.Business.Calculators
{
    public class FPVotingCalculator : IVotingCalculator
    {
        private IElectionRepository _repository;

        public FPVotingCalculator(IElectionRepository repository) : base()
        {
            _repository = repository;
        }

        public ElectionResult CalculateElectionResult(int positionId)
        {
            Position position = _repository.GetPosition(positionId);
            int round = 1;

            // Create the generic result object
            ElectionResult result = new ElectionResult()
            {
                PositionId = positionId,
                RoundResults = new List<ElectionRoundResult>()
            };

            // Set up the first round of voting
            ElectionRoundResult roundResult = new ElectionRoundResult()
            {
                NomineeResults = new List<ElectionRoundNomineeResult>(),
                Round = round
            };

            foreach (Nominee nominee in position.Nominees.Where(n => !position.Election.IsNominationConfirmationRequired || n.HasConfirmedNomination))
            {
                roundResult.NomineeResults.Add(new ElectionRoundNomineeResult()
                {
                    NomineeId = nominee.Id,
                    Votes = 0
                });
            }

            // Repeat until we have a winner with majority
            while (result.SuccessfulNomineeId != 0)
            {
                // Record the votes for this round
                foreach (int userId in position.Votes.Select(v => v.UserId).Distinct())
                {
                    List<Vote> userVotes = position.Votes.Where(v => v.UserId == userId).OrderBy(v => v.PreferenceOrder).ToList();
                    bool voteRecorded = false;
                    for (int i = 0; i < userVotes.Count() && !voteRecorded; i++)
                    {
                        if (!userVotes[i].HasAbstained &&
                            //position.RoleTypeId == userVotes[i].User.MembershipType.R 
                            roundResult.NomineeResults.Any(nr => nr.NomineeId == userVotes[i].NomineeId))
                        {
                            roundResult.NomineeResults.Single(nr => nr.NomineeId == userVotes[i].NomineeId).Votes++;
                            voteRecorded = true;
                        }
                    }

                    if (voteRecorded)
                    {
                        roundResult.TotalVotes++;
                    }
                    else
                    {
                        roundResult.InvalidVotes++;
                    }
                }

                result.RoundResults.Add(roundResult);

                // Do we have a majority winner?
                if (roundResult.NomineeResults.Any(nr => nr.Votes > (roundResult.TotalVotes / 2)))
                {
                    result.SuccessfulNomineeId = roundResult.NomineeResults.Single(nr => nr.Votes > (roundResult.TotalVotes / 2)).NomineeId;
                }
                else
                {
                    // Set up the next round of voting
                    round++;
                    int minimumVotes = roundResult.NomineeResults.Select(nr => nr.Votes).Min();

                    roundResult = new ElectionRoundResult()
                    {
                        NomineeResults = new List<ElectionRoundNomineeResult>(),
                        Round = round
                    };

                    foreach (ElectionRoundNomineeResult nomineeResult in result.RoundResults.Single(rr => rr.Round == (round - 1)).NomineeResults.Where(nr => nr.Votes > minimumVotes))
                    {
                        roundResult.NomineeResults.Add(new ElectionRoundNomineeResult()
                        {
                            NomineeId = nomineeResult.NomineeId,
                            Votes = 0
                        });
                    }
                }
            }

            result.RoundResults.Add(roundResult);

            return result;
        }
    }
}
