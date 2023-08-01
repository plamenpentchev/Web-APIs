using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyBGList_ApiVersion.DTO.v1;

namespace MyBGList_ApiVersion.Controllers.v1
{
    [ApiVersion("1.0")]
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
        [ResponseCache(NoStore = true)]
        public RestDTO<BoardGame[]> Get()
        {
           
            return new RestDTO<BoardGame[]>
            {
                Data = new BoardGame[]
                {
                    new BoardGame
                    {
                        Id = 1,
                        Name="Axis & Allies",
                        Year=1981,
                        MinPlayers=2,
                        MaxPlayers=5
                    },
                    new BoardGame
                    {
                        Id=2,
                        Name="Citadels",
                        Year=2000,
                        MinPlayers=2,
                        MaxPlayers=8
                    },
                    new BoardGame
                    {
                        Id=3,
                        Name="Terraforming Mars",
                        Year=2016,
                        MinPlayers=1,
                        MaxPlayers=5
                    }
                },
                Links = new List<LinkDTO>()
                 {
                     new LinkDTO(
                         href:Url.Action(null, "BoardGames", null, Request.Scheme) ?? "",
                         rel:"self",
                         type:"GET")

                 }

            };
        }
    }
}
