﻿using AdopteUnMatou.API.Models.Users;
using AdopteUnMatou.API.Services.Interfaces;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Register a new user to the database
        /// </summary>
        /// <param name="registerModel"></param>
        /// <response code="400">There was one or more errors during registration validation</response>
        /// <response code="200">Return the newly registrated user's id</response>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterModel registerModel)
        {
            string userId;

            try
            {
                userId = await _authenticationService.RegisterAsync(registerModel);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during user registration: {e.Message}");
            }

            return Ok(userId);
        }
    }
}
