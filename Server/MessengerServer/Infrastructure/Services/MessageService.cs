using Application.IServices;
using Application.Models.MessageDto;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MessageService:IMessageService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;

        public MessageService(IUnitOfWork unit,AuthService auth)
        {
            _unit = unit;

            _auth = auth;
        }

        public async Task<bool> AddMessage(AddMessageDto message)
        {
            var user = await _auth.FindByNameUserAsync(message.UserName);

            if (user != null & !string.IsNullOrEmpty(message.Content))
            {
                user.Messages.Add(new Message()
                {
                    Content = message.Content,
                    TimeCreated = DateTime.Now,
                    UserId = user.Id
                });

                await _unit.Commit();

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
