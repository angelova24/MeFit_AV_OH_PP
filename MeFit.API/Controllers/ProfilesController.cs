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
using MeFit.DAL.Models.DTOs.Profile;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace MeFit.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;
        public ProfilesController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/profile/profile_id
        /// <summary>
        /// Returns detail about current state of the users profile with their goals
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <returns>Profile</returns>
        /// <response code="200">Returns a profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No profile found</response>
        [HttpGet("profile/{id}")]
        //[Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileReadDTO>> GetProfileById([FromRoute] int id)
        {
            var profile = await _context.Profiles.Include(p => p.Goals).FirstOrDefaultAsync(p => p.Id == id);

            if (profile == null)
            {
                return NoContent();
            }
            var profileReadDTO = _mapper.Map<ProfileReadDTO>(profile);
            return Ok(profileReadDTO);
        }

        // PATCH: api/profile/profile_id
        /// <summary>
        /// Executes partial update of the corresponding profile_id.
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <param name="newProfile">Profiles new info</param>
        /// <response code="204">Successfully changed profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No profile found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("profile/{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] JsonPatchDocument<DAL.Models.Domain.Profile> newProfile)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            newProfile.ApplyTo(profile, ModelState);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // POST: api/profile
        /// <summary>
        /// Creates a new profile. Accepts appropriate parameters in the profile body as application/json
        /// </summary>
        /// <param name="newProfile">Profile info</param>
        /// <returns>A newly created profile</returns>
        /// <response code="201">Successfully created profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("profile")]
        //[Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProfileReadDTO>> PostProfile([FromBody] ProfileCreateDTO newProfile)
        {
            var domainProfile = _mapper.Map<DAL.Models.Domain.Profile>(newProfile);
            _context.Profiles.Add(domainProfile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetProfileById", new { id = domainProfile.Id }, _mapper.Map<ProfileReadDTO>(domainProfile));
        }

        // DELETE: api/profile/5 ------------- USER ONLY!!!!! -------------
        /// <summary>
        /// Deletes a profile 
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <response code="204">Successfully deleted profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No profile found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("profile/{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var usernameFromToken = TakeUserNameFromToken();
            if (profile.User.Id != TakeIdFromUser(usernameFromToken).Result)
            {
                return Forbid();
            }

            try
            {
                _context.Profiles.Remove(profile);
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
