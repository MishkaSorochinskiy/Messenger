﻿using System;

namespace Application.Models.MessageDto
{
    public class GetMessageDto
    {
        public string Content { get; set; }

        public DateTime? TimeCreated { get; set; }

        public int UserId { get; set; }
    }
}
