using Dragons.Api.DataAccess;
using Dragons.Api.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dragons.Api.Controllers
{
    // TODO figure out auth
    [Authorize]
    [ApiController]
    [Route("api/dragons")]
    public class DragonController : ControllerBase
    {
        private readonly IDragonRepository _dragonRepository;

        private readonly ILogger<DragonController> _logger;

        public DragonController(
            IDragonRepository dragonRepository,
            ILogger<DragonController> logger)
        {
            _dragonRepository = dragonRepository;
            _logger = logger;
        }

        // GET api/dragons
        // Lists all the dragons in the dragons database
        // Returns them to the user
        [Authorization.Authorize(new[] {"User", "Admin", "Test"})]
        [HttpGet]
        public async Task<IActionResult> ListDragons()
        {
            _logger.LogInformation("Received GET request");

            var dragons =  await _dragonRepository.ListAllDragonsAsync();

            return Ok(dragons);
        }

        // GET api/dragons/{dragon_id}
        // Gets a dragon based on its dragon id
        [HttpGet("{dragonId:guid}")]
        public async Task<IActionResult> GetDragon(Guid dragonId)
        {
            _logger.LogInformation("Received GET request");

            // Guid dragonIdGuid = new Guid(dragonId);

            var result = await _dragonRepository.GetDragonByIdAsync(dragonId);

            return Ok(result);
        }

        // POST api/dragons
        // Creates a dragon in the dragon database
        // Returns a 201 created to the user if successful
        [HttpPost]
        public async Task<IActionResult> CreateDragon([FromBody]Dragon dragon)
        {
            _logger.LogInformation("Received POST request");

            Guid dragonId = Guid.NewGuid();

            Dragon dragonToCreate = new() { 
                DragonId= dragonId,
                Name= dragon.Name,
                Type= dragon.Type,
                Size= dragon.Size,
            };

            var result = await _dragonRepository.InsertDragonAsync(dragonToCreate);

            return result ? StatusCode(201) : StatusCode(500);
        }

        // PUT api/dragons
        // Updates a dragon in the database
        // User needs to give the updated dragon info, with the
        // matching dragon id guid. The guid cannot be updated.
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Dragon dragon)
        {
            _logger.LogInformation("Received PUT request");

            var result = await _dragonRepository.UpdateDragonAsync(dragon);

            return result ? StatusCode(200) : StatusCode(500);
        }

        // DELETE api/dragons
        // Deletes a dragon from the dragons database based on its guid
        // Returns a 204 no content if successful
        [HttpDelete("{dragonId:guid}")]
        public async Task<IActionResult> DeleteDragon(Guid dragonId)
        {
            _logger.LogInformation("Received DELETE request");

            // Guid dragonIdGuid = new Guid(dragonId);

            var result = await _dragonRepository.DeleteDragonAsync(dragonId);

            return result ? StatusCode(204) : StatusCode(500);
        }
    }
}
