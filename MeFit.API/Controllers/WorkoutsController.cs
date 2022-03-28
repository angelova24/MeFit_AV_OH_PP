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
using MeFit.DAL.Models.DTOs.Workout;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutsController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/workouts
        /// <summary>
        /// Gets all workouts with IDs to Sets, Programs and Goals
        /// </summary>
        /// <returns>List of all workouts with IDs to Sets, Programs and Goals</returns>
        /// <response code="200">Returns all workouts</response>
        /// <response code="204">No workouts found</response>
        /// <response code="401">Not authorized</response>
        [HttpGet("workouts")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<WorkoutReadDTO>>> GetWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutReadDTO>>(await _context.Workouts.Include(w => w.Sets).Include(w => w.Programs).Include(w => w.Goals).ToListAsync());

            if (workouts.Count == 0)
            {
                return NoContent();
            }

            return Ok(workouts);
        }

        // GET: api/workout/5
        /// <summary>
        /// Gets workout by ID
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <returns>Workout</returns>
        /// <response code="200">Returns a workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No workout found</response>
        [HttpGet("workout/{id}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WorkoutReadDTO>> GetWorkoutById([FromRoute] int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).Include(w => w.Programs).Include(w => w.Goals).FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WorkoutReadDTO>(workout));
        }

        // GET: api/workout/5/sets
        /// <summary>
        /// Gets a workout with all sets in it
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <returns>Workout</returns>
        /// <response code="200">Returns a workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No workout found</response>
        [HttpGet("workout/{id}/sets")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WorkoutSetDTO>> GetWorkoutWithSets([FromRoute] int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WorkoutSetDTO>(workout));
        }


        // PATCH: api/workout/5 -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Updates workout info
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <param name="newWorkout">Workout's new info</param>
        /// <response code="204">Successfully changed workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No workout found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("workout/{id}")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateWorkout([FromRoute] int id, [FromBody] WorkoutUpdateDTO newWorkout)
        {
            Workout updatedWorkout = _mapper.Map<Workout>(newWorkout);
            if (updatedWorkout.Id != id)
            {
                return BadRequest();
            }
            Workout domainWorkout = await _context.Workouts.FindAsync(id);
            if (domainWorkout == null)
            {
                return NotFound();
            }

            domainWorkout.Name = updatedWorkout.Name;
            domainWorkout.Type = updatedWorkout.Type;
            domainWorkout.OwnerId = updatedWorkout.OwnerId;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }


        // PATCH: api/workout/5/AddSets -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Adds sets to a workout
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <param name="sets">List of sets IDs to be added</param>
        /// <response code="204">Successfully added sets to a workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No workout found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("workout/{id}/AddSets")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSets([FromRoute] int id, [FromBody] List<int> sets)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            foreach (var setId in sets)
            {
                var set = await _context.Sets.FirstOrDefaultAsync(s => s.Id == setId);
                if (set != null)
                {
                    workout.Sets.Add(set);
                }
            }
            var domainWorkout = _mapper.Map<Workout>(workout);

            _context.Entry(domainWorkout).State = EntityState.Modified;

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

        // POST: api/workout -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Creates new workout
        /// </summary>
        /// <param name="newWorkout">New workout info</param>
        /// <returns>A newly created workout</returns>
        /// <response code="201">Successfully created workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("workout")]
        [Authorize(Roles = "contributor, administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<WorkoutReadDTO>> PostWorkout([FromBody] WorkoutCreateDTO newWorkout)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

            var domainWorkout = _mapper.Map<Workout>(newWorkout);
            domainWorkout.OwnerId = userId;
            _context.Workouts.Add(domainWorkout);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetWorkoutById", new { id = domainWorkout.Id }, _mapper.Map<WorkoutReadDTO>(domainWorkout));
        }

        // DELETE: api/workout/5 -------------CONTRIBUTOR ONLY!!!!! AND ONLY OWNED ONES!!! -------------
        /// <summary>
        /// Deletes a workout
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <response code="204">Successfully deleted workout</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No workout found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("workout/{id}")]
        [Authorize(Roles = "contributor, administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            var usernameFromToken = TakeUserNameFromToken();
            if (workout.OwnerId != TakeIdFromUser(usernameFromToken).Result)
            {
                return Forbid();
            }

            try
            {
                _context.Workouts.Remove(workout);
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
