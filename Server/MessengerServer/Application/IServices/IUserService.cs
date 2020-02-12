using Application.Models.UserDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<GetUserDto> GetUserInfoAsync(GetUserInfoRequest request);

        Task<bool> UpdateUserAsync(UpdateUserDto model);

        Task<List<SearchUserDto>> SearchUserAsync(SearchUserDtoRequest request);
    }
}
