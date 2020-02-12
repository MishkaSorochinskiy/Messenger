using Application.IServices;
using Application.Models.PhotoDto;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;

        private readonly IMapper _map;

        private readonly IHostingEnvironment _env;

        private readonly List<string>extensions=new List<string> { ".jpg", ".png",".jpeg"};
        public PhotoService(IUnitOfWork unit, AuthService auth,IMapper map,IHostingEnvironment env)
        {
            _unit = unit;

            _auth = auth;

            _map = map;

            _env = env;
        }

        public async Task<bool> ChangePhotoAsync(AddPhotoDto model)
        {
            var user = await _auth.FindByNameUserAsync(model.UserName);

            var ext = model.UploadedFile.FileName.Substring(model.UploadedFile.FileName.LastIndexOf('.'));

            if (user != null&&this.extensions.Contains(ext))
            {
                var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(user.Id);

                photo.Name = $"{user.Id}{model.UploadedFile.Name}";

                photo.Path = $"{_env.WebRootPath}\\avatars\\{photo.Name}";

                using (var fileStream = new FileStream(photo.Path, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(fileStream);
                }

                await _unit.Commit();

                return true;
            }

            return false;

        }

        public async Task<GetPhotoDto> GetPhotoAsync(string username)
        {
            var user = await _auth.FindByNameUserAsync(username);

            if (user != null)
            {
               return _map.Map<GetPhotoDto>(await _unit.PhotoRepository.GetPhotoByUserAsync(user.Id));
            }

            return default(GetPhotoDto);
        }

        public async Task<GetPhotoDto> GetPhotoAsync(int id)
        {            
            var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(id);

            if (photo != null)
                return  _map.Map<GetPhotoDto>(photo);

            return default(GetPhotoDto);

        }
    }
}
