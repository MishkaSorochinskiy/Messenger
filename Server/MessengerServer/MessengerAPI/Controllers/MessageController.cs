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
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        private readonly IMapper _mapper;

        private readonly IMessageService _masservice;

        public MessageController(IUnitOfWork unit,IMapper mapper,IMessageService masservice)
        {
            _unit = unit;

            _mapper = mapper;

            _masservice = masservice;

        }

        [HttpGet]
        [Authorize]
        public AllMessagesDto Get()
        {
            return _masservice.GetAllMessages();
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<AllMessagesDto> GetChatMessages([FromQuery]GetChatMessagesRequest request)
        {
            var responce=await this._masservice.GetMessagesByChat(request);

            return responce;
        }
    }
}