using Application.IServices;
using Application.Models.PhotoDto;
using AutoMapper;
using Domain;
using Domain.Exceptions.PhotoExceptions;
using Domain.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly IUnitOfWork _unit;

        private readonly IAuthService _auth;

        private readonly IMapper _map;

        private readonly IHostingEnvironment _env;
        
        private readonly IConfiguration _config;

        public PhotoService(IUnitOfWork unit, IAuthService auth,IMapper map,IHostingEnvironment env,IConfiguration config)
        {
            _unit = unit;

            _auth = auth;

            _map = map;

            _env = env;

            _config = config;
        }

        public async Task ChangePhotoAsync(AddPhotoDto model)
        {
            var user = await _auth.FindByIdUserAsync(model.UserId);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!", 400);

            var ext = model.UploadedFile.FileName.Substring(model.UploadedFile.FileName.LastIndexOf('.'));

            if (this._config[$"PhotoExtensions:{ext}"]!=null)
            {
                var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(user.Id);

                photo.Name = $"{user.Id}{model.UploadedFile.Name}";

                photo.Path = $"{_env.WebRootPath}\\avatars\\{photo.Name}";

                using (var fileStream = new FileStream(photo.Path, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(fileStream);
                }

                await _unit.Commit();
            }
            else
            {
                throw new PhotoInCorrectException("Given extension is incorrect!!", 400);
            }
        }

        public async Task<GetPhotoDto> GetPhotoAsync(int userId)
        {
            var user = await _auth.FindByIdUserAsync(userId);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!",400);

            var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(user.Id);

            if (photo == null)
                throw new PhotoNotExistException("Given user haven't got any photos!!",400);

            return _map.Map<GetPhotoDto>(photo);
        }

    }
}
