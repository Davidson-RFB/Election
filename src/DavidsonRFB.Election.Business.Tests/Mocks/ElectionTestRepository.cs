using System;
using System.Collections.Generic;
using System.Linq;
using DavidsonRFB.Election.Business.Models;
using DavidsonRFB.Election.Business.Repositories;

namespace DavidsonRFB.Election.Business.Tests.Mocks
{
    public abstract class ElectionTestRepository : IElectionRepository
    {
        protected Models.Election _election;

        public ElectionTestRepository()
        {
            PopulateElectionWithTestData();
        }

        public IList<Models.Election> GetElections()
        {
            throw new NotImplementedException();
        }

        public Position GetPosition(int positionId)
        {
            return _election.Positions.Single(p => p.Id == positionId);
        }

        protected abstract void PopulateElectionWithTestData();
    }
}
