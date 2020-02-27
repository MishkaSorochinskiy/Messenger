using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.PhotoDto;
using Domain;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                    UserId = (int)HttpContext.Items["id"],
                    UploadedFile = collection.Files[0]
                });

                return Ok();
            }
           
            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetPhoto()
        {
            var userId = (int)HttpContext.Items["id"];

            var photo=await _photoService.GetPhotoAsync(Convert.ToInt32(userId));

            return Ok(photo.Name);
        }
    }
}