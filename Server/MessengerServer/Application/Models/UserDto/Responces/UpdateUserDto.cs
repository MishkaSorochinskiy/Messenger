using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.UserDto
{
    public class UpdateUserDto
    {
        [EmailAddress]
        public string Email { get; set; }
 
        [Range(0,100)]
        public int Age { get; set; }

        [Required]
        public string NickName { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
