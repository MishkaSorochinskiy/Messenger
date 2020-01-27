using Application.IServices;
using Application.Models.MessageDto;
using AutoMapper;
using Domain;
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

        public Chat(IMessageService messageService,IMapper map)
        {
            _messageService = messageService;

            _map = map;
        }
        public async Task SendToAll(AddMessageDto message)
        {
            message.UserName = Context.User.Identity.Name;

            if (await _messageService.AddMessage(message))
            {
                await Clients.All.SendAsync("update",new GetMessageDto() {Content=message.Content});
            }
        }
    }
}
