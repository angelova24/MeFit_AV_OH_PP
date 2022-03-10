//using MeFitAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Primitives;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authorization;
//using MeFit.DAL.Models.Data;
//using MeFit.DAL.Models.DTOs.GoalsDTO;
//using MeFit.DAL.Models.Domain;

//namespace MeFitAPI.Controllers
//{
//    [Authorize]
//    [Route("/goals")]
//    [ApiController]
//    public class GoalController : Controller
//    {
//        private readonly MeFitDbContext _meFitContext;
//        private readonly AutoMapper.IMapper _mapper;


//        public GoalController(MeFitDbContext context, AutoMapper.IMapper mapper)
//        {
//            _meFitContext = context;
//            _mapper = mapper;
//        }

//        public IActionResult updateGoal(int goalId, [FromBody] GoalUpdateDTO dto)
//        {
//            StringValues tokenBase64;
//            HttpContext.Request.Headers.TryGetValue("Authorization", out tokenBase64);
//            var jwt = tokenBase64.ToArray()[0].Split(" ")[1];

//            var handler = new JwtSecurityTokenHandler();
//            var jsonToken = handler.ReadToken(jwt);
//            var tokenS = jsonToken as JwtSecurityToken;

//            var userID = tokenS.Claims.ToArray()[5].Value;

//            if (!_meFitContext.Goals.Any(goal => goal.Id == goalId))
//            {
//                return NotFound();
//            }

//            Goal oldGoal = _meFitContext.Goals
//                .Where(goal => goal.Id == goalId)
//                .Include(goal => goal.Workouts)
//                .Include(goal => goal.Profile)
//                .FirstOrDefault();

//            var authorized = false;

//            if (tokenS.Payload.ToArray()[14].Value.ToString().Contains("mefit-admin"))
//            {
//                authorized = true;
//            }

//            if (!authorized)
//            {
//                return StatusCode(403);
//            }

//            try
//            {

//                _meFitContext.Entry(oldGoal).State = EntityState.Modified;
//                _meFitContext.SaveChanges();
//            }
//            catch
//            {
//                return StatusCode(500);
//            }


//            var updatedGoal = _mapper.Map<Goal>(oldGoal);

//            return Ok(updatedGoal);
//        }