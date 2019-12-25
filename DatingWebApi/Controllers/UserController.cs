using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingWebApi.DTO;
using DatingWebApi.Models;
using DatingWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var resultToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(resultToReturn);
        }

        [HttpGet("{id}")]
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
    }
}