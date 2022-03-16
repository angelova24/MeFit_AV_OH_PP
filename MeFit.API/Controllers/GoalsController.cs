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

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
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
        /// <summary>
        /// Gets list of all goals with workouts in it
        /// </summary>
        /// <returns>List of goals</returns>
        /// <response code="200">Returns all goals</response>
        /// <response code="204">No goals found</response>
        // GET: api/Goals
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GoalReadDTO>>> GetGoals()
        {
            var goalReadDTO = _mapper.Map<List<GoalReadDTO>>(await _context.Goals.Include(w => w.Workouts).ToListAsync());

            if (goalReadDTO.Count == 0)
            {
                return NoContent();
            }

            return Ok(goalReadDTO);
        }

        // GET: api/Goals/5
        /// <summary>
        /// Gets goal by ID
        /// </summary>
        /// <param name="id">ID of a goal</param>
        /// <returns>Goal</returns>
        /// <response code="200">Returns a goal</response>
        /// <response code="404">No goal found</response>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoalReadDTO>> GetGoalById([FromRoute] int id)
        {
            var goalReadDTO = _mapper.Map<GoalReadDTO>( await _context.Goals.Include(g => g.Workouts).ThenInclude(g => g.Complete).FirstOrDefaultAsync(g => g.Id == id));

            if (goalReadDTO == null)
            {
                return NotFound();
            }

            return Ok(goalReadDTO);
        }

        /// <summary>
        /// Updates the corresponding goal to achieved
        /// </summary>
        /// <param name="id">ID of a goal</param>       
        /// <param name="profId">ID of a profile</param>
        /// <response code="204">Successfully changed goal to achieved</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">No goal/profile found</response>
        // PATCH: api/Goals/5/SetAchieved
        [HttpPatch("{id}/SetAchieved")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateGoalAchieved([FromBody] int profId, [FromRoute] int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            var profile = await _context.Profiles.Include(p => p.Goals).FirstOrDefaultAsync(pi => pi.Id == profId);

            if (profile == null)
            {
                return NotFound();
            }
            else if (goal == null)
            {
                return NotFound();
            }
            else if (!profile.Goals.Any(p => p.Id == id))
            {
                return BadRequest();
            }

            profile.Goals.FirstOrDefault(g => g.Id == id).Achieved = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Goals
        /// <summary>
        /// Creates a goal
        /// </summary>
        /// <param name="newGoal">Goals info</param>
        /// <returns>A newly created goal</returns>
        /// <response code="201">Successfully created goal</response>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GoalReadDTO>> PostExercise([FromBody] GoalCreateDTO newGoal)
        {
            var domainGoal = _mapper.Map<Goal>(newGoal);
            _context.Goals.Add(domainGoal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseById", new { id = domainGoal.Id }, _mapper.Map<GoalReadDTO>(domainGoal));
        }

        /// <summary>
        /// Adds list of goals in profile
        /// </summary>
        /// <param name="id">ProfileID</param>
        /// <param name="goals">GoalsIDs to be added</param>
        /// <response code="204">Successfully added goals to profile</response>
        /// <response code="404">No profile found</response>
        // POST: api/Goals/AddToProfile5
        // Vily: little bit confused about this endpoint
        [HttpPost("AddToProfile{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateGoal([FromRoute] int id, [FromBody] List<int> goals)
        {
            var profile = await _context.Profiles.Include(p => p.Goals).FirstOrDefaultAsync(pi => pi.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            foreach (var goalid in goals)
            {
                var tempGoal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalid);
                if (tempGoal != null)
                {
                    profile.Goals.Add(tempGoal);
                }

            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a goal by ID
        /// </summary>
        /// <param name="id">ID of a goal</param>
        /// <response code="204">Successfully deleted goal</response>
        /// <response code="404">No goal found</response>
        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
