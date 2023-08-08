using System.ComponentModel.DataAnnotations;

namespace MyBGList_ApiVersion.Models
{
    public class BoardGames_Categories
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Key]
        [Required]
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public BoardGame? BoardGame { get; set; }

        public Category? Category { get; set; }
    }
}
