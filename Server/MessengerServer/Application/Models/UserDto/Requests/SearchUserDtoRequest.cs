using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.UserDto
{
   public class SearchUserDtoRequest
    {
        [Required]
        public string Filter { get; set; }

        public int UserId { get; set; }
    }
}
