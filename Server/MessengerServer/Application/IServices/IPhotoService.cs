using Application.Models.PhotoDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
   public interface  IPhotoService
    {
        Task ChangePhoto(AddPhotoDto model);

        Task<GetPhotoDto> GetPhoto(string username);
    }
}
