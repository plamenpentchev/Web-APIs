﻿using System.ComponentModel.DataAnnotations;

namespace MyBGList_ApiVersion.Models
{
    public class BoardGames_Domains
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Key]
        [Required]
        public int DomainId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        //navigation properties
        public BoardGame? BoardGame { get; set; }
        public Domain? Domain { get; set; }
    }
}