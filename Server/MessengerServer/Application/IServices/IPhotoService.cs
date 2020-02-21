using Application.Models.PhotoDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
   public interface  IPhotoService
    {
        Task ChangePhotoAsync(AddPhotoDto model);

        Task<GetPhotoDto> GetPhotoAsync(string username);
    }
}
