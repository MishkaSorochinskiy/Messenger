using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.UserDto
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string PhotoName { get; set; }

        public string NickName { get; set; }
    }
}
