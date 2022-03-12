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
using MeFit.DAL.Models.DTOs.ProfileDTO;
using System.Net.Mime;

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
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

        /// <summary>
        /// Returns detail about current state of the users profile
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <returns>Profile</returns>
        /// <response code="200">Returns a profile</response>
        /// <response code="404">No profile found</response>
        // GET: api/Profiles/profile_id
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileReadDTO>> GetProfileById([FromRoute] int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }
            var profileReadDTO = _mapper.Map<ProfileReadDTO>(profile);
            return Ok(profileReadDTO);
        }

        /// <summary>
        /// Executes partial update of the corresponding profile_id.
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <param name="profile"></param>
        /// <response code="204">Successfully changed profile</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">No profile found</response>
        // PATCH: api/Profiles/profile_id
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutProfile([FromRoute] int id, [FromBody] ProfileUpdateDTO profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new profile. Accepts appropriate parameters in the profile body as application/json
        /// </summary>
        /// <param name="newProfile">Profile info</param>
        /// <returns>A newly created profile</returns>
        /// <response code="201">Successfully created profile</response>
        // POST: api/Profiles
        [HttpPost]
        [Consumes("application/json")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProfileReadDTO>> PostProfile([FromBody] ProfileCreateDTO newProfile)
        {
            var domainProfile = _mapper.Map<DAL.Models.Domain.Profile>(newProfile);
            _context.Profiles.Add(domainProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = domainProfile.Id }, newProfile);
        }

        /// <summary>
        /// Deletes a profile
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <response code="204">Successfully deleted profile</response>
        /// <response code="404">No profile found</response>
        // DELETE: api/Profiles/5 ------------- USER ONLY!!!!! -------------
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
