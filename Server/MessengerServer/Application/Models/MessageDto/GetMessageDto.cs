using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.MessageDto
{
    public class GetMessageDto
    {
        public string Content { get; set; }

        public DateTime? TimeCreated { get; set; }
    }
}
