using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList_ApiVersion.Models
{
    [Table("Domains")]
    public class Domain
    {
        [Key]
        [Required] 
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public int LastModifiedDate { get; set; }

        public ICollection<BoardGames_Domains>? BoardGames_Domains { get; set; }

        //Excercises 4.4
        [MaxLength(200)]
        public string? Notes { get; set; } = default!;
        [Required]
        public int Flags { get; set; }

    }
}
