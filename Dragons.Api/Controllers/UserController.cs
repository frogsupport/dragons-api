using Dragons.Api.Authentication;
using Dragons.Api.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dragons.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET api/users
        // Lists all the users in the users database
        // Returns them to the user
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            _logger.LogInformation("Received GET request");

            var users = await _userRepository.ListAllUsersAsync();

            return Ok(users);
        }

        // GET api/users/{username}
        // Gets a users based on its username
        [HttpGet("{username}")]
        public async Task<IActionResult> GetDragon(string username)
        {
            _logger.LogInformation("Received GET request");

            var result = await _userRepository.GetUserByUsernameAsync(username);

            return Ok(result);
        }
    }
}
