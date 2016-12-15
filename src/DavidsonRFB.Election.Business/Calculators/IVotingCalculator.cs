using DavidsonRFB.Election.Business.Models;

namespace DavidsonRFB.Election.Business.Calculators
{
    public interface IVotingCalculator
    {
        ElectionResult CalculateElectionResult(int positionId);
    }
}
