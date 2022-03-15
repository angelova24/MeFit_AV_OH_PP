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
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace MeFit.API.Controllers
{
    [Route("api")]
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
        /// <response code="200">Returns all exercises</response>
        /// <response code="204">No exercises found</response>
        [HttpGet("exercises")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercises()
        {
            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.ToListAsync());

            if (exercises.Count == 0)
            {
                return NoContent();
            }

            return Ok(exercises);
        }

        // GET: api/exercises/contributor
        /// <summary>
        /// Gets all exercises from specific contributor
        /// </summary>
        /// <returns>List of all exercises</returns>
        /// <response code="200">Returns all exercises</response>
        /// <response code="204">No exercises found</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        [HttpGet("exercises/contributor")]
        [Authorize(Roles = "contributor, administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercisesFromContributor()
        {
            var usernameToken = TakeUserNameFromToken();
            var id = TakeIdFromUser(usernameToken);

            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.Where(x => x.OwnerId == id.Result).ToListAsync());

            if (exercises.Count == 0)
            {
                return NoContent();
            }

            return Ok(exercises);
        }

        // GET: api/Exercises/TargetMuscleGroup
        /// <summary>
        /// Gets all exercises ordered by target muscle group
        /// </summary>
        /// <returns>List of all exercises ordered by target muscle group</returns>
        /// <response code="200">Returns all exercises ordered by target muscle group</response>
        /// <response code="204">No exercises found</response>
        [HttpGet("exercises/TargetMuscleGroup")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercisesByTargetMuscleGroup()
        {
            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.OrderBy(e => e.TargetMuscleGroup).ToListAsync());

            if (exercises.Count == 0)
            {
                return NoContent();
            }

            return Ok(exercises);
        }

        // GET: api/Exercises/5
        /// <summary>
        /// Gets exercise by ID
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <returns>Exercise</returns>
        /// <response code="200">Returns an exercise</response>
        /// <response code="404">No exercise found</response>
        [HttpGet("exercise/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExerciseReadDTO>> GetExerciseById([FromRoute] int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ExerciseReadDTO>(exercise));
        }

        // PUT: api/Exercises/5 -------------CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Updates exercises info
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <param name="exercise">Exercises new info</param>
        /// <response code="204">Successfully changed exercise</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No exercise found</response>
        [Authorize(Roles = "contributor, administrator")]
        [HttpPut("exercise/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// Creates an exercise
        /// </summary>
        /// <param name="newExercise">Exercises info</param>
        /// <returns>A newly created exercise</returns>
        /// <response code="201">Successfully created exercise</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        [HttpPost("exercise")]
        [Authorize(Roles = "contributor, administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ExerciseReadDTO>> PostExercise([FromBody] ExerciseCreateDTO newExercise)
        {
            var usernameToken = TakeUserNameFromToken();
            var id = TakeIdFromUser(usernameToken);
    
            var domainExercise = _mapper.Map<Exercise>(newExercise);
            domainExercise.OwnerId = id.Result;
            _context.Exercises.Add(domainExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseById", new { id = domainExercise.Id }, _mapper.Map<ExerciseReadDTO>(domainExercise));
        }

        // DELETE: api/Exercises/5 ------------- CONTRIBUTOR ONLY!!!!! -------------
        /// <summary>
        /// Deletes a specific exercise
        /// </summary>
        /// <param name="id">ID of an exercise</param>
        /// <response code="204">Successfully deleted exercise</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(not having the necessary permissions)</response>
        /// <response code="404">No exercise found</response>
        [Authorize(Roles = "contributor, administrator")]
        [HttpDelete("exercise/{id}")]
        //[Authorize(Roles = "administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            var usernameFromToken = TakeUserNameFromToken();
            if (exercise.OwnerId != TakeIdFromUser(usernameFromToken).Result)
            {
                return Forbid();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
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
