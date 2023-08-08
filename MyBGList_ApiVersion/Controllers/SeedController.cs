using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList_ApiVersion.Models;
using MyBGList_ApiVersion.Models.Csv;
using System.Globalization;

namespace MyBGList_ApiVersion.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SeedController(
            IWebHostEnvironment env,
            ApplicationDbContext context, 
            ILogger<SeedController> logger)
        {
            _webHostEnvironment = env;
            _context = context;
            _logger = logger;
        }
        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Index()
        {
            // Set-up csv helper
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            var pth = System.IO.Path.Combine(
                _webHostEnvironment.ContentRootPath, 
                "Data/bgg_dataset.csv");

            using (var reader = new StreamReader(pth))
            {
                using (var csvReader = new CsvReader(reader, config))
                {
                    var existingBoardGames = await _context.BoradGames.ToDictionaryAsync(bg => bg.Id);
                    var existingDomians = await _context.Domains.ToDictionaryAsync(d => d.Id);
                    var existingMechanics =await _context.Mechanics.ToDictionaryAsync(m => m.Id);
                    var now = DateTime.Now;

                    //Execute block
                    var records = csvReader.GetRecords<BggRecord>();
                    var skippedRow = 0;
                    foreach (var record in records)
                    {
                        if (!record.ID.HasValue || string.IsNullOrEmpty(record.Name) || existingBoardGames.ContainsKey(record.ID)
                        {
                            skippedRow++;
                            continue;
                        }

                    }
                }
            }

           

            return Ok();
        }
    }
}
