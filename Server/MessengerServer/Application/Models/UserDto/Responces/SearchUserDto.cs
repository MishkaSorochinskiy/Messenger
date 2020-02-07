using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.UserDto
{
    public class SearchUserDto
    {
        public int id { get; set; }

        public string Photo { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }
    }
}
