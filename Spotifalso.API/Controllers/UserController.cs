using Microsoft.AspNetCore.Mvc;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotifalso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> Get()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> Get(Guid id)
        {
            return await _userService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<UserViewModel> Post([FromBody] UserInput userInput)
        {
            return await _userService.InsertAsync(userInput);
        }

        [HttpPut("{id}")]
        public async Task<UserViewModel> Put(Guid id, [FromBody] UserInput userInput)
        {
            return await _userService.UpdateAsync(id, userInput);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            NoContent();
        }
    }
}
