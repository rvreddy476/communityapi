using Community.Core.Interfaces;
using Community.Infrastructure.Identity;
using Community.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Community.API.Controllers
{
    [ApiController]
    public class UserController(
      UserManager<ApplicationUser> userManager,
      ILogger<WeatherForecastController> logger,
      ITokenClaimsService tokenClaimsService
      ) : ControllerBase
    {
        [Route("api/register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    UserName = userRegistrationModel.Email,
                    Email = userRegistrationModel.Email,
                    FullName = userRegistrationModel.FirstName + " " + userRegistrationModel.LastName,
                };

              var result =  await userManager.CreateAsync(user, userRegistrationModel.Password);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex?.Message);
                throw;
            }
        }
        [Route("api/login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            try
            {

                var user = await userManager.FindByEmailAsync(userLoginModel.Email);
                
                var isValidPassword = await userManager.CheckPasswordAsync(user, userLoginModel.Password);

                if(!isValidPassword)
                    return Unauthorized(new { Message = "Invalid username or password" });

                if (user != null && isValidPassword)
                {
                    var token = await tokenClaimsService.GetTokenAsync(userLoginModel.Email);
                    return Ok(new UserLoginResponse
                    {
                        IsSuccess = true,
                        Token = token
                    });
                }

                return NotFound(new ErrorDetails
                {
                    Message = "User not found.",

                });


            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex?.Message);
                throw;
            }
        }
    }
}
