using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            CommunitiesCreated = new HashSet<Community>();
            UserCommunities = new HashSet<UserCommunity>();
            Comments = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(20)]
        public string Role { get; set; } = "User";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; } = true;

        // Relaciones
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Community> CommunitiesCreated { get; set; }
        public virtual ICollection<UserCommunity> UserCommunities { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}