using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.PhotoDto
{
    public class GetPhotoDtoRequest
    {
        [Required]
        public int id { get; set; }
    }
}
