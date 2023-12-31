﻿namespace MyBGList_ApiVersion.DTO.v1
{
    public class BoardGameDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = default!;
        public int? Year { get; set; } = default!;
        public int? MinPlayers { get; set; } = default!;
        public int? MaxPlayers { get; set; } = default!;
    }
}
