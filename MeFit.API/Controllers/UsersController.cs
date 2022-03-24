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
using System.Net.Mime;

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

        // GET: api/user
        /// <summary>
        /// Gets user
        /// </summary>
        /// <returns>Redirect Url in Headers</returns>
        /// <response code="303">Redirect URL</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<ActionResult> GetUser()
        {
            //takes username and fullname from token
            var usernameFromToken = TakeUserNameFromToken();
            var nameFromToken = User.Claims.FirstOrDefault(c => c.Type == "name");
            var userId = 0;

            //find user in DB with the same username
            var userDB = await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameFromToken);

            if (userDB == null)
            {
                //creates new user
                var newUser = new UserCreateDTO() { Username = usernameFromToken, Name = nameFromToken.Value };
                var domainNewUser = _mapper.Map<User>(newUser);
                try
                {
                    _context.Users.Add(domainNewUser);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                //get the new user id
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameFromToken);
                userId = user.Id;
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

        // GET: api/user/5
        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User</returns>
        /// <response code="200">Returns a user</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">No user found</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        // POST: api/user
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns>A newly created user</returns>
        /// <response code="201">Successfully created user</response>
        /// <response code="204">User already exists</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserReadDTO>> PostUser()
        {
            //takes username and fullname from token
            var usernameFromToken = TakeUserNameFromToken();
            var nameFromToken = User.Claims.FirstOrDefault(c => c.Type == "name").Value;

            //search for user in DB with the same username
            var userDB = await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameFromToken);

            if (userDB == null)
            {
                //creates new user
                var newUser = new UserCreateDTO() { Username = usernameFromToken, Name = nameFromToken };
                var domainNewUser = _mapper.Map<User>(newUser);

                try
                {
                    _context.Users.Add(domainNewUser);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return CreatedAtAction("GetUserById", new { id = domainNewUser.Id }, _mapper.Map<UserReadDTO>(newUser));
            }

            return NoContent();
        }

        // PATCH: api/user/5      
        /// <summary>
        /// Updates an user by ID
        /// </summary>
        /// <param name="id">ID of an user</param>
        /// <param name="newUser">New users info</param>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed(you're not the owner)</response>
        /// <response code="404">No user found</response>
        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO newUser)
        {
            if (id != newUser.Id)
            {
                return BadRequest();
            }
            //check which user uses the endpoint
            var usernameToken = TakeUserNameFromToken();
            var userId = TakeIdFromUser(usernameToken).Result;
            //check if user is in DB
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            //check if the user is owner, if not -> 403
            if (userId != id)
            {
                return Forbid();
            }
            //change user info in DB
            var domainUser = _mapper.Map<User>(newUser);
            _context.Entry(domainUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // DELETE: api/user/id --------------SELF AND ADMIN-------------
        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">ID of an user</param>
        /// <response code="204">Successfully deleted user</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">No user found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            //find user in DB
            var user = await _context.Users.Include(p => p.Profile).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            //check if user is owner
            var usernameFromToken = TakeUserNameFromToken();
            if (user.Username != usernameFromToken)
            {
                return Forbid();
            }
            //find users profile
            var profile = await _context.Profiles.Include(p => p.Goals).FirstOrDefaultAsync(p => p.Id == user.ProfileId);
            try
            {
                _context.Users.Remove(user);
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
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
