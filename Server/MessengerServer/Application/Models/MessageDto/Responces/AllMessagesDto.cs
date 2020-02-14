using Application.Models.UserDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.MessageDto
{
    public class AllMessagesDto
    {
        public List<GetUserDto> Users { get; set; }

        public List<GetMessageDto> Messages { get; set; }
    }
}
