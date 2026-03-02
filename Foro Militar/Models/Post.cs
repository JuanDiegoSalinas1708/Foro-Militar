using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Posts")]
    public class Post
    {
        public Post()
        {
            PostCategories = new HashSet<PostCategory>();
            Comments = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        [MaxLength(200)]
        public string Country { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        [Required]
        public int MainCategoryId { get; set; }
        public virtual Category MainCategory { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Relaciones
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}