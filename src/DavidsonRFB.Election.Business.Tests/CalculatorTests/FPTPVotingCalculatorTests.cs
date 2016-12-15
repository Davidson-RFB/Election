using DavidsonRFB.Election.Business.Calculators;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;
using DavidsonRFB.Election.Business.Tests.Mocks;
using Xunit;

namespace DavidsonRFB.Election.Business.Tests.CalculatorTests
{
    public class FPTPVotingCalculatorTests
    {
        IElectionRepository _repository = new FPTPElectionTestRepository();

        [Fact]
        public void Test1()
        {
            IVotingCalculator calculator = new FPTPVotingCalculator(_repository);
            ElectionResult result = calculator.CalculateElectionResult(1);

            Assert.NotNull(result);
        }
    }
}
