using System.ComponentModel.DataAnnotations;

namespace MyBGList_ApiVersion.Models
{
    public class BoardGames_Publishers
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Required]
        public int PublisherId { get; set; }

        public BoardGame? BoardGame { get; set; } 

        public Publisher? Publisher { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
