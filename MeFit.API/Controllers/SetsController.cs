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
using MeFit.DAL.Models.DTOs.Set;

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;
        public SetsController(MeFitDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
    }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetReadWithIdDTO>>> GetSets()
        {
            var sets = _mapper.Map<List<SetReadWithIdDTO>>(await _context.Sets.ToListAsync());
            return sets;
        }

        // GET: api/Sets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetReadDTO>> GetSet(int id)
        {
            
            var sets = _mapper.Map<SetReadDTO>(await _context.Sets.FindAsync(id));

            if (sets == null)
            {
                return NotFound();
            }

            return sets;
        }

        // PUT: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(int id, Set @set)
        {
            if (id != @set.Id)
            {
                return BadRequest();
            }

            _context.Entry(@set).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(Set @set)
        {
            _context.Sets.Add(@set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = @set.Id }, @set);
        }

        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSet(int id)
        {
            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(@set);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetExists(int id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
