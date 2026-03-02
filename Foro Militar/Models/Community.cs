using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Communities")]
    public class Community
    {
        public Community()
        {
            Posts = new HashSet<Post>();
            UserCommunities = new HashSet<UserCommunity>();
            Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; } = true;

        // Relaciones
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserCommunity> UserCommunities { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}