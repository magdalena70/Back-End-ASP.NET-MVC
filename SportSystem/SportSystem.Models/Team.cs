namespace SportSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.HomeMatches = new HashSet<Match>();
            this.AwayMatches = new HashSet<Match>();
            this.Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Website { get; set; }

        public DateTime? DateFounded { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Match> HomeMatches { get; set; }

        public virtual ICollection<Match> AwayMatches { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}