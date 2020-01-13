using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingWebApi.DTO;
using DatingWebApi.Helper;
using DatingWebApi.Models;
using DatingWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggedHelperExtension))]
    public class UserController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userParams.UserId = userId;
            if (string.IsNullOrEmpty(userParams.Gender))
            {
                var userFromRepo = await _repo.GetUser(userId);
                userParams.Gender = (userFromRepo.Gender.ToLower() == "male") ? "female" : "male";
            }
            
            var users = await _repo.GetUsers(userParams);
            var resultToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            Response.AddPaging(users.CurrentPage, users.PageSize, users.TotalPages, users.TotalCount);
            return Ok(resultToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var resultToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(resultToReturn);
        }

        [HttpPost(Name = "add")]
        public void AddUser(User user)
        {
            _repo.Add<User>(user);
        }

        [HttpPost(Name = "delete")]
        public void DeleteUser(User user)
        {
            _repo.Delete<User>(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(id);
            if(user != null)
            {                
               _mapper.Map(userForUpdateDto, user);
                if (await _repo.SaveAll())
                    return NoContent();

                throw new Exception($"Updating user {id} failed on save");
            }

            throw new Exception($"User with {id} does not exist");
        }

    }
}