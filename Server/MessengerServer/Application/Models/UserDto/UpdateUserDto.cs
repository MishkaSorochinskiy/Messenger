using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.UserDto
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public int Age { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }
    }
}
