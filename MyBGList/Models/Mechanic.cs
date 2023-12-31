﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList.Models
{
    [Table("Mechanics")]
    public class Mechanic
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null;
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public int LastModifiedDate { get; set; }

        public ICollection<BoardGames_Mechanics>? BoardGames_Mechanics { get; set; }
    }
}
