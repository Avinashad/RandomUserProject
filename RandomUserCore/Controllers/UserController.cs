using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;
using RandomUserCore.Models.IntegrationModels;
using RandomUserCore.Services;

namespace RandomUserCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="userId">The Id used to Search</param>
        /// <returns>Matching User</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Get User by Name
        /// </summary>
        /// <param name="pagination">Pagination object containing limit and skip</param>
        /// /// <param name="searchValue">The value user use to search, That can be either First Name or Last Name</param>
        /// <returns>List result of all the user with matching parameter of pagination or search Value, Returns all Is Not Passed</returns>
        [HttpGet("get-user-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserList([FromQuery] Pagination pagination, [FromQuery] string searchValue)
        {
            return Ok(await _userService.GetUsersList(pagination, searchValue));
        }


        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Returns with created User</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var createdUser = await _userService.CreateUser(user);
            if (createdUser == null)
            {
                return BadRequest();
            }
            return Ok(createdUser);
        }


        /// <summary>
        /// Inset User from Api if we want random datas in the database for test.
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-random-user")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRandomUsers()
        {
            var isCreated = await _userService.CreateRandomUsers();
            if (isCreated) return NoContent();
            return BadRequest();

        }


        /// <summary>
        /// Update User by userId
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="user">User</param>
        /// <returns>Returns updated User</returns>
        [HttpPut("update/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(Guid userId, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null)
            {
                return BadRequest();
            }
            return Ok(updatedUser);


        }


        /// <summary>
        /// Delete User by ID
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var isDeleted = await _userService.DeleteUser(userId);
            if (isDeleted) return NoContent();
            return BadRequest();
        }














    }
}
