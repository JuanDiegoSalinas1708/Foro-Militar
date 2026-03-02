using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("UserCommunities")]
    public class UserCommunity
    {
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        [Required]
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}