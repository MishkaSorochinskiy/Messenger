using Application.IServices;
using Application.Models.MessageDto;
using AutoMapper;
using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Hubs
{
    [Authorize]
    public class Chat:Hub
    {
        private readonly IMessageService _messageService;

        private readonly IAuthService _auth;

        private readonly IUnitOfWork _unit;

        private readonly IUserService _userService;

        public Chat(IMessageService messageService,IAuthService auth,IUnitOfWork unit,IUserService userService)
        {
            _messageService = messageService;

            _auth = auth;

            _unit = unit;

            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
           var userId=(await this._auth.FindByNameAsync(Context.User.Identity.Name)).UserId;

           var userChats =await this._unit.ChatRepository.GetUserChatsAsync(userId);

            userChats.ForEach(async chat =>
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
            });

           await base.OnConnectedAsync();
        }

        public async Task SendToAll(AddMessageDto message)
        {
            message.UserName = Context.User.Identity.Name;

            if(await this._userService.CheckStatusAsync(message))
            {
                var newmessage = await _messageService.AddMessageAsync(message);

                await Clients.Group(message.chatId.ToString()).SendAsync("update", newmessage, message.chatId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = (await this._auth.FindByNameAsync(Context.User.Identity.Name)).UserId;

            var userChats = await this._unit.ChatRepository.GetUserChatsAsync(userId);

            userChats.ForEach(async chat =>
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());
            });

            await base.OnDisconnectedAsync(exception);
        }
    }
}
