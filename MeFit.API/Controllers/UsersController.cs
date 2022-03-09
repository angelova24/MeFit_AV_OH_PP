using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.UserDTOs;
using AutoMapper;

namespace MeFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<ActionResult<UserReadDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var userReadDTO = _mapper.Map<UserReadDTO>(user);
            return userReadDTO;
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,[FromHeader] UserUpdateDTO user)
        {
           
            if (id != user.Id)
            {
                return BadRequest();
            }

            var domainUser = _mapper.Map<User>(user);
            _context.Entry(domainUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser(UserCreateDTO user)
        {
            var domainUser = _mapper.Map<User>(user);
            _context.Users.Add(domainUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = domainUser.Id }, _mapper.Map<UserReadDTO>(domainUser));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
