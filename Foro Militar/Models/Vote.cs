using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Votes")]
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }

        public int? CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        [Required]
        public int VoteType { get; set; } // 1 = Upvote, -1 = Downvote

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}