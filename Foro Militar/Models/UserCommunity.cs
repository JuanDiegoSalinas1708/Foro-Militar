using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("UserCommunities")]
    public class UserCommunity
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        [Required]
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}