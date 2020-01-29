using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.PhotoDto;
using Domain;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoservice;

        private readonly IUnitOfWork _unit;
        public PhotoController(IPhotoService photoservice,IUnitOfWork unit)
        {
            _photoservice = photoservice;

            _unit = unit;

        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePhoto(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                await _photoservice.ChangePhoto(new AddPhotoDto()
                {
                    UserName = User.Identity.Name,
                    UserPhoto = await collection.Files[0].getBytes()
                });     
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPhoto()
        {
            var photo=await _photoservice.GetPhoto(User.Identity.Name);

            if (photo != null)
                return new FileContentResult(photo.UserPhoto, new MediaTypeHeaderValue("application/octet-stream"));

            return BadRequest();
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPhotoById([FromQuery]GetPhotoDtoRequest request)
        {
            if (ModelState.IsValid)
            {
                var photo = await _photoservice.GetPhoto(request.id);

                if (photo != null)
                    return new FileContentResult(photo.UserPhoto, new MediaTypeHeaderValue("application/octet-stream"));
            }

            return BadRequest();
        }
    }
}