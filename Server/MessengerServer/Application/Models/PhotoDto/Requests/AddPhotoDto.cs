using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Application.Models.PhotoDto
{
    public class AddPhotoDto
    {
        public string UserName { get; set; }

        [Required]
        public IFormFile UploadedFile { get; set; }
    }
}
