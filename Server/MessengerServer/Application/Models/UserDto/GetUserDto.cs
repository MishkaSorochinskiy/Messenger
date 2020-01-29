using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.UserDto
{
    public class GetUserDto
    {
        public byte[] UserPhoto { get; set; }

        public int Id { get; set; }
    }
}
