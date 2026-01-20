using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers.Login
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController() { }

        [HttpPost]
        public ActionResult Login(string Username, string Password) 
        {
            // Placeholder for login logic
            if(Username != "admin" || Password != "123456")
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            var token = Helper.GenerateToken.Generate("1","Admin");
            return Ok(new {
                Token = token,
                Message = "Login successful"
            });
        }
    }
}
