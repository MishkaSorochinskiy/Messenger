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
        Task<GetMessageDto> AddMessage(AddMessageDto message);

        AllMessagesDto GetAllMessages();

        Task<AllMessagesDto> GetMessagesByChat(GetChatMessagesRequest request);
    }
}
