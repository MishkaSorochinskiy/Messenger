using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.ChatDto.Requests;
using Application.Models.MessageDto;
using AutoMapper;
using Domain;
using Infrastructure;
using MessengerAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMapper mapper,IMessageService messageService)
        {
            _messageService = messageService;

        }

        [HttpGet]
        [Authorize]
        public async Task<AllMessagesDto> GetChatMessages([FromQuery]GetChatMessagesRequest request)
        {
            var responce=await this._messageService.GetMessageByChatAsync(request);

            return responce;
        }
    }
}