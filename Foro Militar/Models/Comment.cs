using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Relaciones
        public virtual ICollection<Comment> Replies { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}