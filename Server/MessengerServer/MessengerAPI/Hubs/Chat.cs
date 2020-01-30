using Application.IServices;
using Application.Models.MessageDto;
using AutoMapper;
using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Hubs
{
    public class Chat:Hub
    {
        private readonly IMessageService _messageService;

        private readonly IMapper _map;

        private readonly AuthService _auth;

        private readonly IUnitOfWork _unit;

        public Chat(IMessageService messageService,IMapper map,AuthService auth,IUnitOfWork unit)
        {
            _messageService = messageService;

            _map = map;

            _auth = auth;

            _unit = unit;
        }
        public async Task SendToAll(AddMessageDto message)
        {
            message.UserName = Context.User.Identity.Name;

            var newmessage = await _messageService.AddMessage(message);

            if (newmessage!=null)
            {
                await Clients.All.SendAsync("update",newmessage);
            }
        }
    }
}
