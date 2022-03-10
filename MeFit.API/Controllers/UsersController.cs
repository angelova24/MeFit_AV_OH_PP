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


        //We need a get from header after Keyloack

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<ActionResult<UserReadDTO>> GetUser([FromHeader] int id)
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
        /// Makes a partial update to the user object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PATCH: api/Users/:user_id     
        [HttpPatch("{id}")]
        //[Consumes("application/json")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO user)
        {

            //Returns true or false, if passwords are the same returns true
            // var isSamePassword = _context.Users.Any(e => e.Password == user.Password);
            //if (!isSamePassword)
            //{
            //    return BadRequest(400);
            //}

            var domainUser = _mapper.Map<User>(user);
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(domainUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return BadRequest(400);
                }
                else
                {
                    throw;
                }
            }

            return BadRequest(400);
        }
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/Users
        //[HttpPost]        
        //public async Task<IActionResult> PostUser(UserCreateDTO user)
        //{
        //    var domainUser = _mapper.Map<User>(user);
        //    _context.Users.Add(domainUser);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = domainUser.Id }, user);
        //}


        //------------------------------------------Self only Admin-----------------
        // POST: api/Users/user_id/update_password
        [HttpPost]
        //[Consumes("application/json")]
        public async Task<IActionResult> PutUPostUserWithPasswordser(int id, UserUpdatePasswordDTO user)
        {

            var domainUser = _mapper.Map<User>(user);
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(domainUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return BadRequest(400);
                }
                else
                {
                    throw;
                }
            }

            return BadRequest(400);
        }


        // --------------SELF  AND ADMIN-------------
        // DELETE: api/Users/:user_id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //cascade delete user with profile of user

            var user = await _context.Profiles.Where(p => p.UserId == id).Include(upr => upr.User).FirstAsync();
            if (user == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
