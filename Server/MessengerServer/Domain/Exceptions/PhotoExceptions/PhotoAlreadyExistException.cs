using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.PhotoExceptions
{
   public class PhotoAlreadyExistException:BaseException
    {
        public PhotoAlreadyExistException(string message,int statusCode):base(message,statusCode)
        {

        }
    }
}
