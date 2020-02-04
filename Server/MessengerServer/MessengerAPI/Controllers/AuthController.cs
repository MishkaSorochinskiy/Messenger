using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models;
using Infrastructure.AppSecurity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        private readonly UserManager<SecurityUser> _usermanager;
        public AuthController(AuthService auth,UserManager<SecurityUser> usermanager)
        {
            _auth = auth;

            _usermanager = usermanager;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _auth.SignIn(model);

                if (result.Succeeded)
                    return Ok(Response.Headers["set-cookie"]);
                else
                    return BadRequest("Sign in denied!!");
            }

            return BadRequest("Model is not valid!!");
        }

        [HttpGet("[action]")]
        public async Task SignOut()
        {
             await _auth.SignOut();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if(await _auth.FindByNameAsync(model.Email) == null)
                {
                    var result = await _auth.Register(model);

                    if (result.Succeeded)
                        return Ok(Response.Headers["set-cookie"]);
                    else
                        return BadRequest("Register denied!");
                }
            }

            return BadRequest("Model is not valid!");
        }

        [HttpGet("[action]")]
        public bool VerifyToken()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpPost("[action]")]
        public async  Task<bool> EmailExist([FromBody]CheckRegisterModel model)
        {
            return (await _usermanager.FindByEmailAsync(model.Email)) == null;
        }
    }
}