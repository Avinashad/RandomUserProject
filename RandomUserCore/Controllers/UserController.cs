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
        /// Create User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _userService.CreateUser(user);
            return NoContent();
        }


        /// <summary>
        /// Inset User from Api if we want random datas.
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-random-user")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRandomUsers()
        {
            await _userService.CreateRandomUser();
            return NoContent();
        }


        /// <summary>
        /// Update User by userId
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPut("update/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(Guid userId, [FromBody] User user)
        {
            await _userService.UpdateUser(user);
            return NoContent();
        }


        /// <summary>
        /// Delete User by ID
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpDelete("delete/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }


        /// <summary>
        /// Get User by Name
        /// </summary>
        /// <param name="pagination">Pagination</param>
        /// /// <param name="searchValue">SearchValue</param>
        /// <returns></returns>
        [HttpGet("get-user-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserList([FromQuery] Pagination pagination, [FromQuery] string searchValue)
        {
            return Ok(await _userService.GetUsersList(pagination, searchValue));
        }


        /// <summary>
        /// Testing endpoint to get random string object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<IEnumerable<string>> GetString()
        {
            return new string[] { "hi", "this", "is", "working" };

        }










    }
}
