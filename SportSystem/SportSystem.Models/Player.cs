namespace SportSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual Team Team { get; set; }
    }
}