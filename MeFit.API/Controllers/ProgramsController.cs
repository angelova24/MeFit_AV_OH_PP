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

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Programs
        /// <summary>
        /// Gets all programs
        /// </summary>
        /// <returns>List of all programs</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ProgramReadDTO>>> GetPrograms()
        {
            var programs = _mapper.Map<List<ProgramReadDTO>>(await _context.Programs.Include(p => p.Workouts).ToListAsync());

            if (programs.Count == 0)
            {
                return NoContent();
            }

            return Ok(programs);
        }

        // GET: api/Programs/5
        /// <summary>
        /// Gets program by ID
        /// </summary>
        /// <param name="id">ID of a program</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProgramReadDTO>> GetProgramById([FromRoute] int id)
        {
            var program = await _context.Programs.Include(p=> p.Workouts).FirstOrDefaultAsync(p => p.Id == id);

            if (program == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProgramReadDTO>(program));
        }

        // PUT: api/Programs/5/AddWorkouts
        /// <summary>
        /// Add workouts to program
        /// </summary>
        /// <param name="id">ID of a program</param>
        /// <param name="workoutIds">List of workouts IDs to be added</param>
        /// <returns></returns>
        [HttpPatch("{id}/AddWorkouts")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutProgram([FromRoute] int id, [FromBody] List<int> workoutIds)
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
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(id))
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

        // POST: api/Programs
        /// <summary>
        /// Creates new program
        /// </summary>
        /// <param name="newProgram">New program object</param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProgramReadDTO>> PostProgram([FromBody] ProgramCreateDTO newProgram)
        {
            var domainProgram = _mapper.Map<Program>(newProgram);
            _context.Programs.Add(domainProgram);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramById", new { id = domainProgram.Id }, newProgram);
        }

        // DELETE: api/Programs/5
        /// <summary>
        /// Deletes program from DB
        /// </summary>
        /// <param name="id">ID of program</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProgram([FromRoute] int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }
    }
}
