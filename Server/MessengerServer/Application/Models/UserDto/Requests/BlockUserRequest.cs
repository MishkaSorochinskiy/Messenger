using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.UserDto.Requests
{
    public class BlockUserRequest
    {
        [Required]
        public int UserIdToBlock { get; set; }

        public int UserId { get; set; }
    }
}
