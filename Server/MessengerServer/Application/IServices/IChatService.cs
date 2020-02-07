using Application.Models.ChatDto.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IChatService
    {
        Task<bool> CreateChat(AddChatRequest request);
    }
}
