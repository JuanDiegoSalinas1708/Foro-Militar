using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("PostCategories")]
    public class PostCategory
    {
        [Required]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}