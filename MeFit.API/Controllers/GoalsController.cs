using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.GoalsDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
        // GET: api/Goals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalReadDTO>>> GetGoals()
        {
            var goalReadDTO = _mapper.Map<List<GoalReadDTO>>(await _context.Goals.Include(w => w.Workouts).ToListAsync());
            return goalReadDTO;
        }

        /// <summary>
        /// Executes a partial update of the corresponding goal_id Achived with true or false
        /// </summary>
        /// <param name="id">GoalId</param>       
        /// <param name="profId">ProfileID</param>
        /// <returns></returns>
        // PATCH: api/Goals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateGoal(int profId,int id)
        {
            var gol = await _context.Goals.FindAsync(id);
            var profile = await _context.Profiles.Include(p => p.Goals).FirstOrDefaultAsync(pi => pi.Id == profId);
            
            if (profile == null)
            {
                return NotFound();
            }
            if(profile.Goals.Any(i => i.Id== id))
            {
                profile.Goals.FirstOrDefault(g => g.Id == id).Achieved = true; 

            }
            

            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Create new goal in profile
        /// </summary>
        /// <param name="id">ProfileID</param>
        /// <param name="goals">GoalsID to be added</param>
        /// <returns></returns>
        // POST: api/Goals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGoal(int id, List<int> goals)
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
        /// Delete a goal by Id
        /// </summary>
        /// <param name="id">GoalID</param>
        /// <returns></returns>
        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GoalDeleteDTO>> DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            var goalDeleteDTO = _mapper.Map<Goal>(goal);
            _context.Goals.Remove(goalDeleteDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
