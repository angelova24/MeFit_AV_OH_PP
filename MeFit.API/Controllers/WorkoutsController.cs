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

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Workouts
        /// <summary>
        /// Gets all workouts with IDs to Sets, Programs and Goals
        /// </summary>
        /// <returns>List of all workouts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDTO>>> GetWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutReadDTO>>(await _context.Workouts.Include(w => w.Sets).Include(w => w.Programs).Include(w => w.Goals).ToListAsync());

            if (workouts.Count == 0)
            {
                return NoContent();
            }

            return Ok(workouts);
        }

        // GET: api/Workouts/5
        /// <summary>
        /// Gets a workout by ID
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <returns>Workout</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutReadDTO>> GetWorkoutById([FromRoute] int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).Include(w => w.Programs).Include(w => w.Goals).FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return _mapper.Map<WorkoutReadDTO>(workout);
        }

        // PUT: api/Workouts/5/AddSets -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Adds sets to a workout
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <param name="sets">List of sets IDs to be added</param>
        /// <returns></returns>
        [HttpPatch("{id}/AddSets")]
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
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        // POST: api/Workouts -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Creates new workout
        /// </summary>
        /// <param name="newWorkout">New workout object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout([FromBody] WorkoutCreatDTO newWorkout)
        {
            var domainWorkout = _mapper.Map<Workout>(newWorkout);
            _context.Workouts.Add(domainWorkout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkoutById", new { id = domainWorkout.Id }, newWorkout);
        }

        // DELETE: api/Workouts/5 -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Deletes a workout
        /// </summary>
        /// <param name="id">ID of a workout</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
