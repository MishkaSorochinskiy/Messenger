using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.BlockedUserExceptions
{
    public class BlockedUserAlreadyExist:BaseException
    {
        public BlockedUserAlreadyExist(string message,int statusCode):base(message,statusCode)
        {

        }
    }
}
