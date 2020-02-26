using Application;
using Application.IServices;
using Application.Models.MessageDto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MessengerAPI.Hubs
{
    [Authorize]
    public class Chat:Hub
    {
        private readonly IMessageService _messageService;

        private readonly IUnitOfWork _unit;

        private readonly IUserService _userService;

        private readonly ICache _cache;

        public Chat(ICache cache,IMessageService messageService,IUnitOfWork unit,IUserService userService)
        {
            _messageService = messageService;

            _unit = unit;

            _userService = userService;

            _cache = cache;
        }

        public override async Task OnConnectedAsync()
        {
            var userId =Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userChats = await this._unit.ChatRepository.GetUserChatsAsync(Convert.ToInt32(userId));

            userChats.ForEach(async chat =>
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
            });

            await base.OnConnectedAsync();
        }

        public async Task SendToAll(AddMessageDto message)
        {
            message.userId =Convert.ToInt32(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var isblocked = _cache.Get($"{message.userId}:{message.chatId}");

            if (isblocked == null)
            {
                isblocked = await this._userService.CheckStatusAsync(message);

                _cache.Set($"{message.userId}:{message.chatId}", isblocked, TimeSpan.FromMinutes(10));
            }

            if(!(bool)isblocked)
            {
                var newmessage = await _messageService.AddMessageAsync(message);

                await Clients.Group(message.chatId.ToString()).SendAsync("update", newmessage, message.chatId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userChats = await this._unit.ChatRepository.GetUserChatsAsync(Convert.ToInt32(userId));

            userChats.ForEach(async chat =>
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());
            });

            await base.OnDisconnectedAsync(exception);
        }
    }
}
