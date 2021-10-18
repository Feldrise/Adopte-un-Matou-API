using AdopteUnMatou.API.Entities;
using AdopteUnMatou.API.Models.Cats;
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
    public class CatsController : ControllerBase
    {
        private readonly ICatsService _catsService;
        private readonly IMapper _mapper;

        public CatsController(ICatsService catsService, IMapper mapper)
        {
            _catsService = catsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all cats
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<Cat>>> GetCats([FromQuery] string adoptionStatus)
        {
            IList<Cat> cats = await _catsService.GetCats(adoptionStatus);

            return Ok(cats);
        }

        /// <summary>
        /// Get a cat by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cat>> GetCat(string id)
        {
            Cat cat = await _catsService.GetCat(id);

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        /// <summary>
        /// Get a cat image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/image")]
        public async Task<ActionResult<byte[]>> GetCatImage(string id)
        {
            byte[] image = await _catsService.GetCatImage(id);

            if (image == null) return NotFound();

            return Ok(image);
        }

        /// <summary>
        /// Create a cat
        /// </summary>
        /// <param name="catModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<string>> CreateCat([FromBody] CatSubmitModel catModel)
        {
            try
            {
                Cat cat = _mapper.Map<Cat>(catModel);

                string id = await _catsService.CreateCat(catModel.Image, cat);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }

        /// <summary>
        /// Update a cat
        /// </summary>
        /// <param name="catModel"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateCat([FromBody] CatSubmitModel catModel)
        {
            try
            {
                Cat cat = _mapper.Map<Cat>(catModel);

                await _catsService.UpdateCat(catModel.Image, cat);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
