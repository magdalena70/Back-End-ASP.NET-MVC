namespace SportSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public virtual Team Team { get; set; }

        public string UserId { get; set; }

        [Required]
        public virtual User VotingUser { get; set; }
    }
}
