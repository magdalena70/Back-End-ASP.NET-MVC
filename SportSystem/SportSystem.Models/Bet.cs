using System.ComponentModel.DataAnnotations;

namespace SportSystem.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public string BettingUserId { get; set; }

        [Required]
        public virtual User BettingUser { get; set; }

        public virtual Match Match { get; set; }

        public decimal? HomeBet { get; set; }

        public decimal? AwayBet { get; set; }
    }
}