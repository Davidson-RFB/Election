using System;
using System.Linq;
using DavidsonRFB.Election.Business.Models;

namespace DavidsonRFB.Election.Business.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ElectionContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Role Types.
            if (!context.RoleTypes.Any())
            {
                context.RoleTypes.Add(new RoleType() { Description = "Operational" });
                context.RoleTypes.Add(new RoleType() { Description = "Administration" });
            }

            // Look for any Membership Types.
            if (!context.MembershipTypes.Any())
            {
                context.MembershipTypes.Add(new MembershipType() { Description = "Administration" });
                context.MembershipTypes.Add(new MembershipType() { Description = "Junior" });
                context.MembershipTypes.Add(new MembershipType() { Description = "Life Member" });
                context.MembershipTypes.Add(new MembershipType() { Description = "Ordinary Active" });
                context.MembershipTypes.Add(new MembershipType() { Description = "Probationary" });
            }

            // Look for any Voting Rights.
            if (!context.VotingRights.Any())
            {
                context.VotingRights.Add(new VotingRight() { MembershipType = context.MembershipTypes.Single(mt => mt.Description == "Administration"), RoleType = context.RoleTypes.Single(rt => rt.Description == "Administration") });
                context.VotingRights.Add(new VotingRight() { MembershipType = context.MembershipTypes.Single(mt => mt.Description == "Ordinary Active"), RoleType = context.RoleTypes.Single(rt => rt.Description == "Administration") });
                context.VotingRights.Add(new VotingRight() { MembershipType = context.MembershipTypes.Single(mt => mt.Description == "Ordinary Active"), RoleType = context.RoleTypes.Single(rt => rt.Description == "Operational") });
            }

            context.SaveChanges();
        }
    }
}
