using Application.Models.MessageDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IMessageService
    {
        Task<bool> AddMessage(AddMessageDto message);

        AllMessagesDto GetAllMessages();
    }
}
