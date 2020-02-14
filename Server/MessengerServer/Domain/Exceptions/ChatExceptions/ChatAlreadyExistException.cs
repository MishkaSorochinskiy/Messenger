using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.ChatExceptions
{
    public class ChatAlreadyExistException:BaseException
    {
        public ChatAlreadyExistException(string message,int statusCode):base(message,statusCode)
        {

        }
    }
}
