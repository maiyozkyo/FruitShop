using FruitShop.Business.Base;
using FruitShop.Business.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Reflection.Metadata;

namespace FruitShop.Services.User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserBusiness IUserBusiness; 
        public UserController(IUserBusiness iUserBusiness) {
            IUserBusiness = iUserBusiness;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test(RequestMsg requestMsg)
        {
            var cookieOptions = new CookieOptions
            {
                // Set the cookie properties
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = false, // Use "false" if not using HTTPS
                HttpOnly = true,
                Domain = "localhost",
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("testCookie", "Cookie content", cookieOptions);
            return Ok("OK");
        }

        [AllowAnonymous]
        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync(RequestMsg requestMsg)
        {
            if (!string.IsNullOrEmpty(requestMsg.Params))
            {
                var lstParams = JsonConvert.DeserializeObject<List<string>>(requestMsg.Params);
                var phone = lstParams[0];
                var pw = lstParams[1];
                var loginUser = await IUserBusiness.LoginAsync(phone, pw);
                if (loginUser != null)
                {
                    var cookieOptions = new CookieOptions()
                    {
                        HttpOnly = true,
                        IsEssential = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Domain = "localhost", //using https://localhost:44340/ here doesn't work
                        Expires = DateTime.UtcNow.AddDays(14)
                    };


                    Response.Cookies.Append("testCookie", "Cookie content", cookieOptions);
                    return Ok(loginUser);
                }
                else
                {
                    return NotFound("Không tìm thấy user");
                }
            }
            return BadRequest("Sai");
        }

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync(RequestMsg requestMsg)
        {
            Type tType = IUserBusiness.GetType();
            MethodInfo method = tType.GetMethod(requestMsg.Method);
            if (method != null)
            {
                var lstObj = new List<object>();
                var lstParams = JsonConvert.DeserializeObject<List<string>>(requestMsg.Params);

                var paramInfos = method.GetParameters();
                for (var idx = 0; idx < paramInfos.Length; idx++)
                {
                    var type = paramInfos[idx].ParameterType;
                    if (typeof(string).IsAssignableFrom(type))
                    {
                        lstObj.Add(lstParams[idx]);
                    }
                    else
                    {
                        var obj = JsonConvert.DeserializeObject(lstParams[idx], type);
                        lstObj.Add(obj);
                    }

                }

                var res = await (dynamic)method.Invoke(IUserBusiness, lstObj.ToArray());
                return Ok(res);
            }
            return NotFound();
        }
    }
}
