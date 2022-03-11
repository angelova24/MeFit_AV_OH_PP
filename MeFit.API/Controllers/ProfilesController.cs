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
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Profiles/profile_id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileReadDTO>> GetProfile(int id)
        {
            var profile = await _context.Users.FirstOrDefaultAsync(u => u.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }
            var profileReadDTO = _mapper.Map<ProfileReadDTO>(profile);
            return profileReadDTO;
        }

        /// <summary>
        /// Executes partial update of the corresponding profile_id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        // PATCH: api/Profiles/profile_id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        //[Consumes("application/json")]
        public async Task<IActionResult> PutProfile(int id, ProfileUpdateDTO profile)
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
        /// <param name="newProfile"></param>
        /// <returns></returns>
        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ProfileReadDTO>> PostProfile([FromBody]ProfileCreateDTO newProfile)
        {
            var domainProfile = _mapper.Map<MeFit.DAL.Models.Domain.Profile>(newProfile);
            _context.Profiles.Add(domainProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = domainProfile.Id }, newProfile);
        }

        /// <summary>
        /// Deletes a profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
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
