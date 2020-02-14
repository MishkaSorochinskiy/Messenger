using Application.IServices;
using Application.Models.PhotoDto;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions.PhotoExceptions;
using Domain.Exceptions.UserExceptions;
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

        public async Task ChangePhotoAsync(AddPhotoDto model)
        {
            var user = await _auth.FindByNameUserAsync(model.UserName);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!", 400);

            var ext = model.UploadedFile.FileName.Substring(model.UploadedFile.FileName.LastIndexOf('.'));

            if (this.extensions.Contains(ext))
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

        public async Task<GetPhotoDto> GetPhotoAsync(string username)
        {
            var user = await _auth.FindByNameUserAsync(username);

            if (user == null)
                throw new UserNotExistException("Given user not exist!!",400);

            var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(user.Id);

            if (photo == null)
                throw new PhotoNotExistException("Given user haven't got any photos!!",400);

            return _map.Map<GetPhotoDto>(photo);
        }

        public async Task<GetPhotoDto> GetPhotoAsync(int id)
        {            
            var photo = await _unit.PhotoRepository.GetPhotoByUserAsync(id);

            if (photo == null)
                throw new PhotoNotExistException("Given user haven't got any photos!!",400);

            return  _map.Map<GetPhotoDto>(photo);
        }
    }
}
