using Application.Models.MessageDto;
using Application.Models.PhotoDto;
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
        }
    }
}
