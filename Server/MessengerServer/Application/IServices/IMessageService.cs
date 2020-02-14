using Application.Models.ChatDto.Requests;
using Application.Models.MessageDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IMessageService
    {
        Task<GetMessageDto> AddMessageAsync(AddMessageDto message);

        Task<AllMessagesDto> GetAllMessagesAsync();

        Task<AllMessagesDto> GetMessageByChatAsync(GetChatMessagesRequest request);
    }
}
