using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GreythrBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iUserBL;
        public UserController(IUserBL iUserBL)
        {
            this.iUserBL = iUserBL;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = iUserBL.Login(loginModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [HttpPost("Register")]
        public IActionResult Register(ProfileModel2 reg)
        {
            try
            {
                var result = iUserBL.Register(reg);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Registered Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Register!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
    }
}
