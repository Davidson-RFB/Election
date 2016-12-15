using System;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;

namespace DavidsonRFB.Election.Business.Calculators
{
    public class OPVotingCalculator : IVotingCalculator
    {
        private IElectionRepository _repository;

        public OPVotingCalculator(IElectionRepository repository) : base()
        {
            _repository = repository;
        }

        public ElectionResult CalculateElectionResult(int positionId)
        {
            throw new NotImplementedException();
        }
    }
}
