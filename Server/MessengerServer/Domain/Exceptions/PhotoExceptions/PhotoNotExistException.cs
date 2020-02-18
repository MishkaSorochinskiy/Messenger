using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.PhotoExceptions
{
    public class PhotoNotExistException:BaseException
    {
        public PhotoNotExistException(string message,int statusCode):base(message,statusCode)
        {

        }
    }
}
