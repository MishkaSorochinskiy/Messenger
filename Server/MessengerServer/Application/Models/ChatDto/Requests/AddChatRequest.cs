using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.ChatDto.Requests
{
    public class AddChatRequest
    {
        [Required]
        public int SecondUserId { get; set; }

        public string UserName { get; set; }
    }
}
