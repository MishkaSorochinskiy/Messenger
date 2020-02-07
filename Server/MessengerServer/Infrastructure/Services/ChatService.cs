using Application.IServices;
using Application.Models.ChatDto.Requests;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ChatService:IChatService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;
        public ChatService(IUnitOfWork unit,AuthService auth)
        {
            _unit = unit;

            _auth = auth;
        }

        public async Task<bool> CreateChat(AddChatRequest request)
        {
            var user = await _auth.FindByNameUserAsync(request.UserName);

            if((await this._unit.ChatRepository.ChatExist(user.Id, request.SecondUserId)))
            {
                var chat = new Chat()
                {
                    FirstUserId = user.Id,
                    SecondUserId = request.SecondUserId
                };

                await this._unit.ChatRepository.Create(chat);

                await this._unit.Commit();

                return true;
            }

            return false;
        }

        
    }
}
