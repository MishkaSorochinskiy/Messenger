using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public abstract class BaseException:Exception
    {
        public int StatusCode { get; protected set; }
        public BaseException(string message,int statusCode):base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
