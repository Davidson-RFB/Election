using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DavidsonRFB.Election.Business.Models
{
    public class Election
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Date of election")]
        [Required]
        public DateTime ElectionDate { get; set; }

        [DisplayName("Each nomination requires confirmation?")]
        [Required]
        public bool IsNominationConfirmationRequired { get; set; }

        [DisplayName("Voting method")]
        [Required]
        public VotingMethod VotingMethod { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }

    public enum VotingMethod
    {
        FirstPastThePost,
        FullPreferential,
        OptionalPreferential
    }

    public class Position
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Election")]
        [Required]
        public int ElectionId { get; set; }

        public virtual Election Election { get; set; }

        [DisplayName("Role type")]
        [Required]
        public int RoleTypeId { get; set; }

        public virtual RoleType RoleType { get; set; }

        public virtual ICollection<Nominee> Nominees { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }

    public class Nominee
    {
        public int Id { get; set; }

        [DisplayName("Position")]
        [Required]
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }

        [DisplayName("Qualified")]
        [Required]
        public bool IsQualified { get; set; }

        [DisplayName("Has confirmed nomination")]
        [Required]
        public bool HasConfirmedNomination { get; set; }
    }

    public class Vote
    {
        public int Id { get; set; }

        [DisplayName("User")]
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [DisplayName("Position")]
        [Required]
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }

        [DisplayName("Nominee")]
        [Required]
        public int NomineeId { get; set; }

        public virtual Nominee Nominee { get; set; }

        [Required]
        public int PreferenceOrder { get; set; }

        [Required]
        public bool HasAbstained { get; set; }

        [Required]
        public DateTime VoteDateTime { get; set; }
    }

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Firezone Number")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Membership type")]
        public MembershipType MembershipType { get; set; }
    }

    public class MembershipType
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class RoleType
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class VotingRight
    {
        public int Id { get; set; }

        [DisplayName("Membership type")]
        [Required]
        public int MembershipTypeId { get; set; }

        public virtual MembershipType MembershipType { get; set; }

        [DisplayName("Role type")]
        [Required]
        public int RoleTypeId { get; set; }

        public virtual RoleType RoleType { get; set; }
    }
}
