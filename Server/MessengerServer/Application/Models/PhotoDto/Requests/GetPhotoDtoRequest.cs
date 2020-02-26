using System.ComponentModel.DataAnnotations;

namespace Application.Models.PhotoDto
{
    public class GetPhotoDtoRequest
    {
        [Required]
        public int id { get; set; }
    }
}
