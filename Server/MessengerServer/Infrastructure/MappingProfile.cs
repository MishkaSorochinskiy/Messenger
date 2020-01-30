using Application.Models.MessageDto;
using Application.Models.PhotoDto;
using Application.Models.UserDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, GetMessageDto>();

            CreateMap<Photo, GetPhotoDto>();

            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(u => u.Id))
                .ForMember(dest=>dest.PhotoName,src=>src.MapFrom(u=>u.Photo.Name));
        }
    }
}
