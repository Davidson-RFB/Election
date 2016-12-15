using DavidsonRFB.Election.Business.Calculators;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;
using DavidsonRFB.Election.Business.Tests.Mocks;
using Xunit;

namespace DavidsonRFB.Election.Business.Tests.CalculatorTests
{
    public class FPVotingCalculatorTests
    {
        IElectionRepository _repository = new FPElectionTestRepository();

        [Fact]
        public void Test1()
        {
            IVotingCalculator calculator = new FPVotingCalculator(_repository);
            ElectionResult result = calculator.CalculateElectionResult(1);

            Assert.NotNull(result);
        }
    }
}
