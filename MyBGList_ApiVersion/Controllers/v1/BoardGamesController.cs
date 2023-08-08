using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList_ApiVersion.DTO.v1;
using MyBGList_ApiVersion.Models;
using System.Linq;

namespace MyBGList_ApiVersion.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;
        private readonly ApplicationDbContext _context;

        public BoardGamesController(
            ILogger<BoardGamesController> logger, 
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        //[EnableCors]
        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame[]>> Get()
        {
            var bg = _context.BoradGames;
            return new RestDTO<BoardGame[]>
            {
                Data = await bg.ToArrayAsync(),
                Links = new List<LinkDTO>()
                 {
                     new LinkDTO(
                         href:Url.Action(null, "BoardGames", null, Request.Scheme) ?? "",
                         rel:"self",
                         type:"GET")

                 }

            }
    }
}
