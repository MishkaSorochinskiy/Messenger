using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.ChatDto.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatservice;
        public ChatController(IChatService chatservice)
        {
            _chatservice = chatservice;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<bool> Create([FromBody]AddChatRequest request)
        {
            request.UserName = User.Identity.Name;

            return await _chatservice.CreateChat(request);
        }
    }
}