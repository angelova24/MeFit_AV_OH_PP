using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Program;
using AutoMapper;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public ProgramsController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/programs
        /// <summary>
        /// Gets all programs with workouts IDs
        /// </summary>
        /// <returns>List of all programs with workouts IDs</returns>
        /// <response code="200">Returns all programs</response>
        /// <response code="204">No programs found</response>
        /// <response code="401">Not authorized</response>
        [HttpGet("programs")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProgramReadDTO>>> GetPrograms()
        {
            var programs = _mapper.Map<List<ProgramReadDTO>>(await _context.Programs.Include(p => p.Workouts).ToListAsync());

            if (programs.Count == 0)
            {
                return NoContent();
            }

            return Ok(programs);
        }

        // GET: api/program/5
        /// <summary>
        /// Gets program by ID
        /// </summary>
        /// <param name="id">ID of a program</param>
        /// <returns>Program with workouts IDs</returns>
        /// <response code="200">Returns a program</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No program found</response>
        [HttpGet("program/{id}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProgramReadDTO>> GetProgramById([FromRoute] int id)
        {
            var program = await _context.Programs.Include(p => p.Workouts).FirstOrDefaultAsync(p => p.Id == id);

            if (program == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProgramReadDTO>(program));
        }

        // PATCH: api/program/5/AddWorkouts -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Add workouts to program
        /// </summary>
        /// <param name="id">ID of a program</param>
        /// <param name="workoutIds">List of workouts IDs to be added</param>
        /// <response code="204">Successfully added workouts to program</response>
        /// <response code="404">No program found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("program/{id}/AddWorkouts")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddWorkoutsToProgram([FromRoute] int id, [FromBody] List<int> workoutIds)
        {
            var program = await _context.Programs.Include(p => p.Workouts).FirstOrDefaultAsync(p => p.Id == id);

            if (program == null)
            {
                return NotFound();
            }

            foreach (var workoutId in workoutIds)
            {
                var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
                if (workout != null)
                {
                    program.Workouts.Add(workout);
                }
            }
            var domainProgram = _mapper.Map<Program>(program);
            _context.Entry(program).State = EntityState.Modified;

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

        // POST: api/program -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Creates new program
        /// </summary>
        /// <param name="newProgram">New program info</param>
        /// <returns>A newly created program</returns>
        /// <response code="201">Successfully created program</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("program")]
        [Authorize(Roles = "contributor, administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProgramReadDTO>> PostProgram([FromBody] ProgramCreateDTO newProgram)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

            var domainProgram = _mapper.Map<Program>(newProgram);
            domainProgram.OwnerId = userId;
            _context.Programs.Add(domainProgram);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetProgramById", new { id = domainProgram.Id }, _mapper.Map<ProgramReadDTO>(newProgram));
        }

        // DELETE: api/program/5 ------------- CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Deletes a program
        /// </summary>
        /// <param name="id">ID of a program</param>
        /// <response code="204">Successfully deleted program</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No program found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("program/{id}")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProgram([FromRoute] int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            if (program.OwnerId != userId)
            {
                return Forbid();
            }

            try
            {
                _context.Programs.Remove(program);
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
