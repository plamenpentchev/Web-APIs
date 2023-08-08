using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList_ApiVersion.Models
{
    [Table("Publishers")]
    public class Publisher
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }

        public ICollection<BoardGame>? BoardGames { get; set; } = default!;
    }
}
