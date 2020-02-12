using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.UserDto;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly AuthService _auth;

        private readonly IMapper _map;

        public UserController(IUserService userService, AuthService auth, IMapper map)
        {
            _userService = userService;

            _auth = auth;

            _map = map;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _auth.FindByNameUserAsync(User.Identity.Name);

            if (user != null)
            {
                var dto = _map.Map<GetUserDto>(user);

                dto.Email = User.Identity.Name;

                return Ok(dto);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto model)
        {
            var res = await _userService.UpdateUserAsync(model);

            if (res)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<List<SearchUserDto>> Search([FromQuery]SearchUserDtoRequest request )
        {
            request.UserName = User.Identity.Name;

           return await this._userService.SearchUserAsync(request);
        }
    }
}