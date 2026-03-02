using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<PostCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(7)]
        public string ColorHex { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public int? CommunityId { get; set; }
        public virtual Community Community { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; } = true;

        // Relaciones
        public virtual ICollection<PostCategory> Posts { get; set; }
    }
}