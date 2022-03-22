using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using AutoMapper;
using MeFit.DAL.Models.DTOs.Set;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;
        public SetsController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/sets
        /// <summary>
        /// Gets all sets
        /// </summary>
        /// <returns>List of all sets</returns>
        /// <response code="200">Returns all sets</response>
        /// <response code="204">No sets found</response>
        /// <response code="401">Not authorized</response>
        [HttpGet("sets")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SetReadWithIdDTO>>> GetSets()
        {
            var sets = _mapper.Map<List<SetReadWithIdDTO>>(await _context.Sets.ToListAsync());

            if (sets.Count == 0)
            {
                return NoContent();
            }
            return Ok(sets);
        }

        // GET: api/set/5
        /// <summary>
        /// Gets set by ID
        /// </summary>
        /// <param name="id">ID of a set</param>
        /// <returns>Set</returns>
        /// <response code="200">Returns a set</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No set found</response>
        [HttpGet("set/{id}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SetReadWithIdDTO>> GetSetById([FromRoute] int id)
        {
            var set = await _context.Sets.FindAsync(id);

            if (set == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SetReadWithIdDTO>(set));
        }

        // POST: api/set -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Creates a set
        /// </summary>
        /// <param name="newSet">Set info</param>
        /// <returns>A newly created set</returns>
        /// <response code="201">Successfully created set</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("set")]
        [Authorize(Roles = "contributor, administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SetReadWithIdDTO>> PostSet([FromBody] SetReadDTO newSet)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

            var domainSet = _mapper.Map<Set>(newSet);
            domainSet.OwnerId = userId;
            _context.Sets.Add(domainSet);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetSetById", new { id = domainSet.Id }, _mapper.Map<SetReadWithIdDTO>(domainSet));
        }

        // DELETE: api/set/5  ------------- CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Deletes a specific set
        /// </summary>
        /// <param name="id">ID of a set</param>
        /// <response code="204">Successfully deleted set</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No exercise found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("set/{id}")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSet(int id)
        {
            var set = await _context.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            var usernameFromToken = TakeUserNameFromToken();
            if (set.OwnerId != TakeIdFromUser(usernameFromToken).Result)
            {
                return Forbid();
            }

            try
            {
                _context.Sets.Remove(set);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        private string TakeUserNameFromToken()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
            return username.Value;
        }
        private async Task<int> TakeIdFromUser(string usernameFromToken)
        {
            var id = (await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameFromToken)).Id;
            return id;
        }
    }
}
