using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers([FromQuery] bool shouldIncludeSensitiveInfo)
        {
            IList<User> users = await _usersService.GetUsers(shouldIncludeSensitiveInfo);

            return Ok(users);
        }

        /// <summary>
        /// Get a user by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            User user = await _usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Get a user image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/image")]
        public async Task<ActionResult<byte[]>> GetUserImage(string id)
        {
            byte[] image = await _usersService.GetUserImage(id);

            if (image == null) return NotFound();

            return Ok(image);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserSubmitModel userModel)
        {
            if (userModel.Id != id)
            {
                return BadRequest("The id of the user doesn't match the ressource id");
            }

            try
            {
                User user = _mapper.Map<User>(userModel);

                await _usersService.UpdateUser(userModel.ProfilePicture, user);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
