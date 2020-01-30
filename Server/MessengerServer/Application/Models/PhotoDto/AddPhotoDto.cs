using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.PhotoDto
{
    public class AddPhotoDto
    {
        public string UserName { get; set; }

        [Required]
        public IFormFile UploadedFile { get; set; }
    }
}
