using System;
using DavidsonRFB.Election.Business.Calculators;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DavidsonRFB.Election.Web.Controllers
{
    public class PositionController : Controller
    {
        #region Fields

        private readonly IElectionRepository _repository;

        #endregion

        #region Constructors

        public PositionController(IElectionRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public IActionResult Browse(int id)
        {
            return View(_repository.GetPosition(id));
        }

        public IActionResult ElectionResult(int id)
        {
            // Get the Position details
            Position position = _repository.GetPosition(id);

            // Calculate the election results
            IVotingCalculator calculator;
            switch (position.Election.VotingMethod)
            {
                case VotingMethod.FullPreferential:
                    calculator = new FPVotingCalculator(_repository);
                    break;

                case VotingMethod.OptionalPreferential:
                    calculator = new OPVotingCalculator(_repository);
                    break;

                default: // VotingMethod.FirstPastThePost:
                    calculator = new FPTPVotingCalculator(_repository);
                    break;
            }

            return View(calculator.CalculateElectionResult(id));
        }
    }
}
