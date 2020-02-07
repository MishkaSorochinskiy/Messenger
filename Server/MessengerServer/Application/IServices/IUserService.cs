using Application.Models.UserDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<GetUserDto> GetUserInfo(GetUserInfoRequest request);

        Task<bool> UpdateUser(UpdateUserDto model);

        Task<List<SearchUserDto>> SearchUser(SearchUserDtoRequest request);
    }
}
