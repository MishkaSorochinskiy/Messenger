using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure
{
    public class MessageComparer : IEqualityComparer<Message>
    {
        public bool Equals([AllowNull] Message x, [AllowNull] Message y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] Message obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}
