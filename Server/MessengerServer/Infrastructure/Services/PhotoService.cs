using Application.IServices;
using Application.Models.PhotoDto;
using AutoMapper;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly IUnitOfWork _unit;

        private readonly AuthService _auth;

        private readonly IMapper _map;
        public PhotoService(IUnitOfWork unit,AuthService auth,IMapper map)
        {
            _unit = unit;

            _auth = auth;

            _map = map;
        }

        public async Task ChangePhoto(AddPhotoDto model)
        {
            var photos = _unit.PhotoRepository.GetAll();

            var user = await _auth.FindByNameUserAsync(model.UserName);

            if (user != null)
            {
                var photo = await _unit.PhotoRepository.GetPhotoByUser(user.Id);

                if (photo != null)
                {
                    _unit.PhotoRepository.Delete(photo.Id);
                }
                user.Photo = new Photo(model.UserPhoto, user.Id);

                await _unit.Commit();
            }

        }

        public async Task<GetPhotoDto> GetPhoto(string username)
        {
            var user = await _auth.FindByNameUserAsync(username);

            if (user != null)
            {
               return _map.Map<GetPhotoDto>(await _unit.PhotoRepository.GetPhotoByUser(user.Id));
            }

            return await Task.FromResult(default(GetPhotoDto));
        }
    }
}
