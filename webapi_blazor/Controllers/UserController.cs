using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.Helper;
using webapi_blazor.models.EbayDB;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EbayContext _context;
        private readonly JwtAuthService _jwtService;
        public UserController(EbayContext context,JwtAuthService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        
        [HttpPost("/user/login")]
        public async Task<ActionResult> Login(UserLoginVM userLogin)
        {
            //Tìm user trong db có username hoặc email 
            var userCheckLogin = await  _context.Users.SingleOrDefaultAsync(us => us.Username == userLogin.Account || us.Email == userLogin.Account);

            if(userCheckLogin != null && PasswordHelper.VerifyPassword(userLogin.Password, userCheckLogin.PasswordHash)) //Nếu account có trong db (account có thể username hoặc email)    
            {
                //Tao token
                var token = _jwtService.GenerateToken(userCheckLogin);
                UserLoginResult userLoginResult = new UserLoginResult();
                userLoginResult.AccessToken = token;
                userLoginResult.Account = userLogin.Account;
                return Ok(userLoginResult);
                //TRả về kết quả là user name và token
            }
            return BadRequest("Tài khoản mật khẩu không đúng!");
        }

        [Authorize]
        [HttpGet("/user/GetProfile")]
        public async Task<ActionResult> GetProfile([FromHeader] string authorization) {
            //Lấy thông tin user từ token
            var token = HttpContext.Request.Headers["Authorization"];
            return Ok("");
        }
        
        [Authorize(Roles ="Buyer,Seller")]
        [HttpGet("/user/PostNew")]
        public async Task<ActionResult> PostNew() {

            return Ok("");
        }
       
    }
       
}