using Application.Models.PhotoDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
   public interface  IPhotoService
    {
        Task<bool> ChangePhoto(AddPhotoDto model);

        Task<GetPhotoDto> GetPhoto(string username);

        Task<GetPhotoDto> GetPhoto(int id);

    }
}
