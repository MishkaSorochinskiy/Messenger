using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.MessageDto;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        private readonly IMapper _mapper;

        public MessageController(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;

            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<GetMessageDto> Get()
        {
            var tmp = _unit.MessageRepository.GetAll();
            return _mapper.Map<IEnumerable<GetMessageDto>>( _unit.MessageRepository.GetAll());
        }
    }
}