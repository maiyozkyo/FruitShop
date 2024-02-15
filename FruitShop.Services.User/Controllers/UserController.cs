using Microsoft.AspNetCore.Mvc;

namespace FruitShop.Services.User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("test")]
        public async Task<IActionResult> Test()
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
    }
}
