using Application.Models.UserDto;
using System.Collections.Generic;

namespace Application.Models.MessageDto
{
    public class AllMessagesDto
    {
        public List<GetUserDto> Users { get; set; }

        public List<GetMessageDto> Messages { get; set; }
    }
}
