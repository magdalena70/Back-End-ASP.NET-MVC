namespace SportSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Match
    {
        public Match()
        {
            this.Comments = new HashSet<Comment>();
            this.Bets = new HashSet<Bet>();
        }

        public int Id { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
