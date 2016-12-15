using System;
using System.Collections.Generic;
using System.Linq;
using DavidsonRFB.Election.Business.Models;

namespace DavidsonRFB.Election.Business.Tests.Mocks
{
    public class FPElectionTestRepository : ElectionTestRepository
    {
        protected override void PopulateElectionWithTestData()
        {
            Random rnd = new Random();

            // Election object
            _election = new Models.Election() { Description = "Full Preferential Election", ElectionDate = DateTime.Today, Id = 1, IsNominationConfirmationRequired = true, Positions = new List<Position>(), VotingMethod = VotingMethod.FirstPastThePost };

            // Position
            Position position = new Position() { Description = "Test Position", Election = _election, ElectionId = _election.Id, Id = 1, Nominees = new List<Nominee>(), Votes = new List<Vote>() };
            _election.Positions.Add(position);

            // MembershipTypes
            List<MembershipType> membershipTypes = new List<MembershipType>();
            for (int i = 1; i <= 3; i++)
            {
                membershipTypes.Add(new MembershipType() { CanVoteForAdministrationPositions = true, CanVoteForOperationalPositions = true, Description = "Membership type " + i, Id = i });
            }

            // Users
            List<User> users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                MembershipType membershipType = membershipTypes[rnd.Next(membershipTypes.Count)];
                users.Add(new User() { Id = i, MembershipType = membershipType, Name = "User " + i });
            }

            // Nominees
            for (int i = 1; i <= 5; i++)
            {
                User user = users[rnd.Next(users.Count)];
                position.Nominees.Add(new Nominee() { HasConfirmedNomination = true, Id = i, IsQualified = true, Position = position, PositionId = position.Id });
            }

            // Votes
            for (int i = 1; i <= users.Count; i++)
            {
                for (int j = 1; j <= position.Nominees.Count; j++)
                {
                    position.Votes.Add(new Vote() { HasAbstained = false, Id = (i * 10) + j, Nominee = position.Nominees.ToList()[j - 1], NomineeId = position.Nominees.ToList()[j - 1].Id, Position = position, PositionId = position.Id, User = users[i - 1], UserId = users[i - 1].Id, VoteDateTime = DateTime.Now });
                }
            }
        }

    }
}
