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
using MeFit.DAL.Models.DTOs.Exercise;

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public ExercisesController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Exercises
        /// <summary>
        /// Gets all exercises
        /// </summary>
        /// <returns>List of all exercises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercises()
        {
            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.ToListAsync());

            if (exercises.Count == 0)
            {
                return NoContent();
            }

            return exercises;
        }

        // GET: api/Exercises/TargetMuscleGroup
        /// <summary>
        /// Gets all exercises sorted by Target muscle group
        /// </summary>
        /// <returns>List of all exercises sorted by Target muscle group</returns>
        [HttpGet("TargetMuscleGroup")]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercisesByTargetMuscleGroup()
        {
            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.OrderBy(e => e.TargetMuscleGroup).ToListAsync());

            if (exercises.Count == 0)
            {
                return NoContent();
            }

            return exercises;
        }

        // GET: api/Exercises/5
        /// <summary>
        /// Gets exercise by ID
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <returns>Exercise</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseReadDTO>> GetExerciseById([FromRoute] int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExerciseReadDTO>(exercise);
        }

        // PUT: api/Exercises/5 -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Updates exercises info
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <param name="exercise">Exercises new info</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise([FromRoute] int id, [FromBody] ExerciseReadDTO exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            var domainExercise = _mapper.Map<Exercise>(exercise);

            _context.Entry(domainExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/Exercises -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Adds new exercise to DB
        /// </summary>
        /// <param name="newExercise">Exercises info</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ExerciseReadDTO>> PostExercise([FromBody] ExerciseCreateDTO newExercise)
        {
            var domainExercise = _mapper.Map<Exercise>(newExercise);
            _context.Exercises.Add(domainExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseById", new { id = domainExercise.Id }, newExercise);
        }

        // DELETE: api/Exercises/5 ------------- CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Deletes exercise from DB
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
