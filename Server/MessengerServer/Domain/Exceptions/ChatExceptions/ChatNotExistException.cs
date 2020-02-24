using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.ChatExceptions
{
    public class ChatNotExistException:BaseException
    {
        public ChatNotExistException(string message,int statusCode):base(message,statusCode)
        {

        }
    }
}
