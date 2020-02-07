using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ChatDto.Responces
{
   public class GetChatDto
    {
        public int Id { get; set; }

        public string Photo { get; set; }

        public string Content { get; set; }
    }
}
