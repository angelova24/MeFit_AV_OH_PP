using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
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

        // GET: api/profile/id
        /// <summary>
        /// Returns detail about current state of the users profile with their goals
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <returns>Profile</returns>
        /// <response code="200">Returns a profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No profile found</response>
        [HttpGet("profile/{id}")]
        [Authorize]
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

        // PATCH: api/profile/id
        /// <summary>
        /// Executes partial update of the corresponding profile.
        /// </summary>
        /// <param name="id">ID of a profile</param>
        /// <param name="newProfile">Profiles new info</param>
        /// <response code="204">Successfully changed profile</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">No profile found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("profile/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] JsonPatchDocument<DAL.Models.Domain.Profile> newProfile)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

            var profile = await _context.Profiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return NotFound();
            }
            //check if the user is owner of the profile
            if (profile.User.Id != userId)
            {
                return Forbid();
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

        // PUT: api/profile/5      
        /// <summary>
        /// Updates a profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newProfile"></param>
        /// <returns></returns>
        [HttpPut("profile/{id}")]
        public async Task<ActionResult> UpdatePutProfile(int id, [FromBody] ProfileUpdateDTO newProfile)
        {
            if (id != newProfile.Id)
            {
                return BadRequest();
            }
            var domainProfile = _mapper.Map<MeFit.DAL.Models.Domain.Profile>(newProfile);
            _context.Entry(domainProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProfileReadDTO>> PostProfile([FromBody] ProfileCreateDTO newProfile)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

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

            var user = await _context.Users.FindAsync(userId);
            user.ProfileId = domainProfile.Id;
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
        /// <response code="403">Not allowed(you're not the owner of this profile)</response>
        /// <response code="404">No profile found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("profile/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            var usernameFromToken = TakeUserNameFromToken();
            if (profile.User.Username != usernameFromToken)
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
