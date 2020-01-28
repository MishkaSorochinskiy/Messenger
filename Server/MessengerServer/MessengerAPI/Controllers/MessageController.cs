using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IHubContext<Chat> _chat;

        public MessageController(IUnitOfWork unit,IMapper mapper,IHubContext<Chat> chat)
        {
            _unit = unit;

            _mapper = mapper;

            _chat=chat;
        }

        [HttpGet]
        public IEnumerable<GetMessageDto> Get()
        {
            return _mapper.Map<IEnumerable<GetMessageDto>>( _unit.MessageRepository.GetAll());
        }
    }
}