using System;
using System.Collections.Generic;
using System.Linq;
using DavidsonRFB.Election.Business.Data;
using DavidsonRFB.Election.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DavidsonRFB.Election.Business.Repositories
{
    public class ElectionRepository : IElectionRepository
    {
        #region Fields

        private readonly ElectionContext _context;

        #endregion

        #region Constructors

        public ElectionRepository(ElectionContext context)
        {
            _context = context;
        }

        #endregion

        public IList<Models.Election> GetElections()
        {
            throw new NotImplementedException();
        }

        public Position GetPosition(int positionId)
        {
            return _context.Positions.Include(p => p.Nominees)
                                     .Include(p => p.Votes)
                                     .Single(p => p.Id == positionId);
        }
    }
}
