using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.UserDto
{
   public class GetUserInfoRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
