using AdopteUnMatou.API.Entities;
using AdopteUnMatou.API.Models.Applications;
using AdopteUnMatou.API.Services.Interfaces;
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
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationsService _applicationsService;

        public ApplicationsController(IApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        /// <summary>
        /// Get all applications
        /// </summary>
        /// <param name="shouldIncludeContent"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<Application>>> GetApplications([FromQuery] bool shouldIncludeContent, [FromQuery] string userId)
        {
            IList<Application> applications = await _applicationsService.GetApplications(shouldIncludeContent, userId);

            return Ok(applications);
        }

        /// <summary>
        /// Get an application by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Application>> GetApplication(string id)
        {
            Application application = await _applicationsService.GetApplication(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        /// <summary>
        /// Create an application
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateApplication([FromBody] Application application)
        {
            try
            {
                string id = await _applicationsService.CreateApplication(application);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request:{e.Message}");
            }
        }

        /// <summary>
        /// Update an application
        /// </summary>
        /// <param name="id"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateApplication(string id, [FromBody] Application application)
        {
            if (application.Id != id)
            {
                return BadRequest("The id of the application doesn't match the ressource id");
            }

            // TODO: improve security here
            try 
            {
                await _applicationsService.UpdateApplication(application);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
