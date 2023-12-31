﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList_ApiVersion.Models
{
    [Table("Mechanics")]
    public class Mechanic
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public int LastModifiedDate { get; set; }

        public ICollection<BoardGames_Mechanics>? BoardGames_Mechanics { get; set; }

        //Excercises 4.4

        [MaxLength(200)]
        public string? Notes { get; set; } = default!;
        [Required]
        public int Flags { get; set; }
    }
}
