using BL.AppService;
using BL.DTOs;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private AccountAppService _accountAppservice;
        IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController
            (
              IConfiguration config,
              AccountAppService accountAppservice,
              IHttpContextAccessor httpContextAccessor,
               UserManager<User> userManager,
               RoleManager<IdentityRole> roleManager
            )
        {
            _config = config;
            _accountAppservice = accountAppservice;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = await _accountAppservice.Find(login.Email, login.Password);
            if (user != null)
            {
                var tokenString = _accountAppservice.CreateToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterationDto userAccount)
        {
            IdentityRole role = new IdentityRole("User");
            var roles = await _roleManager.CreateAsync(role);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            userAccount.Role = "User";
            var user = await _accountAppservice.Register(userAccount);

            if (user.Succeeded)
            {
                var currentUser = await _accountAppservice.FindByName(userAccount.UserName);
                var userId = currentUser.Id;
                await _accountAppservice.AssignToRole(userId, "User");
                return Ok();
            }
            else
                return BadRequest(user.Errors.ToList()[0]);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("AdminRegister")]
        public async Task<IActionResult> RegisterAdmin(RegisterationDto userAccount)
        {
            IdentityRole role = new IdentityRole("Admin");
            var roles = await _roleManager.CreateAsync(role);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            userAccount.Role = "Admin";
            var user = await _accountAppservice.Register(userAccount);
            if (user.Succeeded)
            {
                var currentUser = await _accountAppservice.FindByName(userAccount.UserName);
                var userId = currentUser.Id;
                await _accountAppservice.AssignToRole(userId, "Admin");
                return Ok();
            }
            else
                return BadRequest(user.Errors.ToList()[0]);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var res = _accountAppservice.GetAccountById(id);
            return Ok(res);
        }
        [HttpGet("GetUserName/{id}")]
        public IActionResult GetUserName(string id)
        {
            var res = _accountAppservice.GetAccountById(id);
            return Ok(res.UserName);
        }

        [Authorize]
        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = _accountAppservice.GetAccountById(userID);
            return Ok(res);
        }
        
        
        
        
       
       
    }
}
