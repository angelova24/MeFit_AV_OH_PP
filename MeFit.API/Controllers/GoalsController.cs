using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Goal;
using AutoMapper;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public GoalsController(MeFitDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/goals
        /// <summary>
        /// Gets all goals owned by the user
        /// </summary>
        /// <returns>List of all goals</returns>
        /// <response code="200">Returns all goals</response>
        /// <response code="204">No goals found</response>
        /// <response code="401">Not authorized</response>
        [HttpGet("goals")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GoalReadDTO>>> GetGoals()
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;

            var goals = _mapper.Map<List<GoalReadDTO>>(await _context.Goals.Include(g => g.Workouts).Include(g => g.Profile).ThenInclude(p => p.User).Where(g => g.Profile.User.Id == userId).ToListAsync());

            if (goals.Count == 0)
            {
                return NoContent();
            }

            return Ok(goals);
        }

        // GET: api/goal/5
        /// <summary>
        /// Gets goal by ID
        /// </summary>
        /// <param name="id">ID of a goal</param>
        /// <returns>Goal</returns>
        /// <response code="200">Returns a goal</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">No goal found</response>
        [HttpGet("goal/{id}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoalWorkoutsDTO>> GetGoalById([FromRoute] int id)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            //check if goal exists
            var goal = _mapper.Map<GoalWorkoutsDTO>(await _context.Goals.Include(g => g.Workouts).FirstOrDefaultAsync(g => g.Id == id));
            if (goal == null)
            {
                return NotFound();
            }
            //check if the user is owner of the goal
            var profile = await _context.Profiles.Include(p => p.User).Include(p => p.Goals).Where(p => p.User.Id == userId).FirstOrDefaultAsync();
            if (!profile.Goals.Any(g => g.Id == goal.Id))
            {
                return Forbid();
            }

            return Ok(goal);
        }

        // PATCH: api/goal/5/SetAchieved
        /// <summary>
        /// Updates the corresponding goal to achieved
        /// </summary>
        /// <param name="id">ID of a goal</param>       
        /// <response code="204">Successfully changed goal to achieved</response>
        /// <response code="400">Bad request</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">No goal/profile found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("goal/{id}/SetAchieved")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateGoalAchieved([FromRoute] int id)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            //check if goal exists
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            //check if the user is owner of the goal
            var profile = await _context.Profiles.Include(p => p.User).Include(p => p.Goals).Where(p => p.User.Id == userId).FirstOrDefaultAsync();
            if (!profile.Goals.Any(g => g.Id == goal.Id))
            {
                return Forbid();
            }

            profile.Goals.FirstOrDefault(g => g.Id == id).Achieved = true;

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
        // PATCH: api/workout/5/AddWorkouts
        /// <summary>
        /// Adds workouts to goal
        /// </summary>
        /// <param name="id">ID of a goal</param>
        /// <param name="workouts">List of workouts IDs to be added</param>
        /// <response code="204">Successfully added workouts to a goal</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No goal found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("goal/{id}/AddWorkouts")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddSets([FromRoute] int id, [FromBody] List<int> workouts)
        {
            var goal = await _context.Goals.Include(g => g.Workouts).FirstOrDefaultAsync(g => g.Id == id);

            if (goal == null)
            {
                return NotFound();
            }

            foreach (var workoutId in workouts)
            {
                var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
                if (workout != null)
                {
                    var goalWorkout = new GoalWorkoutCreateDTO() { Complete = false, GoalId = id, WorkoutId = workout.Id };
                    var domainGoalWorkout = _mapper.Map<GoalWorkout>(goalWorkout);
                    try
                    {
                        _context.GoalWorkouts.Add(domainGoalWorkout);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }

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

        // POST: api/goal
        /// <summary>
        /// Creates a goal
        /// </summary>
        /// <param name="newGoal">Goals info</param>
        /// <returns>A newly created goal</returns>
        /// <response code="201">Successfully created goal</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("goal")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GoalReadDTO>> PostGoal([FromBody] GoalCreateDTO newGoal)
        {
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            var profile = await _context.Profiles.Include(p => p.User).Where(p => p.User.Id == userId).FirstOrDefaultAsync();

            var domainGoal = _mapper.Map<Goal>(newGoal);
            domainGoal.ProfileId = profile.Id;
            _context.Goals.Add(domainGoal);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetGoalById", new { id = domainGoal.Id }, _mapper.Map<GoalReadDTO>(domainGoal));
        }

        // DELETE: api/goal/5
        /// <summary>
        /// Deletes a goal by ID
        /// </summary>
        /// <param name="id">ID of a goal</param>
        /// <response code="204">Successfully deleted goal</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">No goal found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("goal/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            //check if the user is owner of the goal
            var profile = await _context.Profiles.Include(p => p.User).Include(p => p.Goals).Where(p => p.User.Id == userId).FirstOrDefaultAsync();
            if (!profile.Goals.Any(g => g.Id == goal.Id))
            {
                return Forbid();
            }

            try
            {
                _context.Goals.Remove(goal);
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
