using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.DAL.Models.Data;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.API.Controllers
{
    [Route("api/user")]
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
        /// Gets user
        /// </summary>
        /// <returns>Redirect Url in Headers</returns>
        // GET: api/user
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<ActionResult> GetUser()
        {
            //takes username and fullname from token
            var usernameFromToken = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
            var nameFromToken = User.Claims.FirstOrDefault(c => c.Type == "name");
            var userId = 0;

            //search for user in DB with the same username
            var userDB = await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameFromToken.Value);

            if (userDB == null)
            {
                //creates new user
                var newUser = new UserCreateDTO() { Username = usernameFromToken.Value, Name = nameFromToken.Value };
                var domainNewUser = _mapper.Map<User>(newUser);
                var createdUser = _context.Users.Add(domainNewUser);
                userId = createdUser.CurrentValues.GetValue<int>("Id");
                await _context.SaveChangesAsync();
            }
            else
            {
                userId = userDB.Id;
            }
            string location = Url.Action(nameof(GetUserById), null, new { id = userId }, "https");
            location = location.Replace("http://", "https://");
            Response.Headers.Add("Location", location);
            return StatusCode(StatusCodes.Status303SeeOther);
        }


        

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        // GET: api/Users/5
        [Authorize]        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDTO>> GetUserById(int id)
        {
             
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var userReadDTO = _mapper.Map<UserReadDTO>(user);
            return Ok(userReadDTO);

        }
        //------------------------------------------Self only Admin-----------------
        /// <summary>
        /// Update to the users password
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PATCH: api/Users/user_id/update_password
        [Authorize]
        [HttpPatch("{id}")]        
        //[Consumes("application/json")]
        //public async Task<ActionResult<UserUpdatePasswordDTO>> PutUser(int id, UserUpdatePasswordDTO user)
        //{
            
        //    var userDb = await _context.Users.FindAsync(id);
            
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }
        //    userDb.Password = user.Password;
            
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return BadRequest(400);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

            
        //    return NoContent();
        //}


       /// <summary>
       /// Creates a new user
       /// </summary>
       /// <param name="newUser"></param>
       /// <returns>Status code 201 created and the created User by id</returns>
        // POST: api/Users
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        public async Task<ActionResult<UserCreateDTO>> PostUser([FromBody]UserCreateDTO newUser)
        {
            
            var domainnewUser = _mapper.Map<MeFit.DAL.Models.Domain.User>(newUser);
            _context.Users.Add(domainnewUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { id = domainnewUser.Id }, newUser);
        }


        //------------------------------------------Self only Admin-----------------
        // POST: api/Users/user_id/update_password
        //[HttpPatch]
        //[Consumes("application/json")]
        //public async Task<ActionResult<User>> PutUPostUserWithPasswordser(int id, UserUpdatePasswordDTO user)
        //{

        //    var domainUser = _mapper.Map<User>(user);
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(domainUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return BadRequest(400);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return domainUser;
        //}

        /// <summary>
        /// Deletes user (cascade - user's profile)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // --------------SELF  AND ADMIN-------------
        // DELETE: api/Users/:user_id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            //cascade delete user with profile of user

            var user = await _context.Users.Include(p => p.Profile).FirstOrDefaultAsync(u => u.Id == id);
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
