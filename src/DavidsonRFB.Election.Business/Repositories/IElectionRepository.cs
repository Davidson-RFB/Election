using System.Collections.Generic;

namespace DavidsonRFB.Election.Business.Repositories
{
    public interface IElectionRepository
    {
        IList<Models.Election> GetElections();
        Models.Position GetPosition(int positionId);

    }
}