using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foro.Entities.Models
{
    [Table("PostCategories")]
    public class PostCategory
    {
        [Key, Column(Order = 0)]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}