using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBGList_ApiVersion.DTO.v2;

namespace MyBGList_ApiVersion.Controllers.v2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class BoardGamesController : ControllerBase
    {
        private ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        //[EnableCors]
        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
        public RestDTO<BoardGameDTO[]> Get()
        {
            return new RestDTO<BoardGameDTO[]>
            {
                Items = new BoardGameDTO[]
                {
                    new BoardGameDTO
                    {
                        Id = 1,
                        Name="Axis & Allies",
                        Year=1981,
                        MinPlayers=2,
                        MaxPlayers=5
                    },
                    new BoardGameDTO
                    {
                        Id=2,
                        Name="Citadels",
                        Year=2000,
                        MinPlayers=2,
                        MaxPlayers=8
                    },
                    new BoardGameDTO
                    {
                        Id=3,
                        Name="Terraforming Mars",
                        Year=2016,
                        MinPlayers=1,
                        MaxPlayers=5
                    }
                },
                Links = new List<DTO.v1.LinkDTO>()
                 {
                     new DTO.v1.LinkDTO(
                         href:Url.Action(null, "BoardGames", null, Request.Scheme) ?? "",
                         rel:"self",
                         type:"GET")

                 }

            };
        }
    }
}
