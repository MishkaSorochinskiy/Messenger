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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormCollection collection)
        {
            if (ModelState.IsValid && collection.Files[0] != null)
            {
                await _photoService.ChangePhotoAsync(new AddPhotoDto()
                {
                    UserName = User.Identity.Name,
                    UploadedFile = collection.Files[0]
                });

                return Ok();
            }
           
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPhoto()
        {
            var photo=await _photoService.GetPhotoAsync(User.Identity.Name);

            return Ok(photo.Name);
        }
    }
}