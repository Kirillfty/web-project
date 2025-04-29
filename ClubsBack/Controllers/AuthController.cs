using System.ComponentModel.DataAnnotations;
using ClubsBack.Entities;
using ClubsBack.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace TwitterBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtCreator _jwtCreator;
        private readonly IRepository<User> _userRepository;

        public AuthController(JwtCreator jwtCreator, IRepository<User> userRepository)
        {
            _jwtCreator = jwtCreator;
            _userRepository = userRepository;
        }

        public record LoginRequest([Required] string NickName, [Required] string Password);

        public record RegisterRequest(
            [Required] string FirstName,
            [Required] string LastName,
            [Required] string NickName,
            [Required] string Password);
        
        public record TokenPair(string AccessToken, string RefreshToken);

        [HttpPost("login")]
        public ActionResult<TokenPair> Login([FromBody] LoginRequest loginRequest)
        {
            var users = _userRepository.Get();

            var user = users.FirstOrDefault(u => u.NickName == loginRequest.NickName);

            if (user == null)
                return Unauthorized("Username is incorrect");

            if (user.Password != loginRequest.Password)
                return Unauthorized("Password is incorrect");

            string refreshToken = Guid.NewGuid().ToString();
            
            _userRepository.SetRefreshToken(refreshToken, user.NickName);

            string authToken = _jwtCreator.Create(user.Role,user.Id);
            
            return new TokenPair(authToken, refreshToken);
        }

        [Authorize]
        [HttpGet("check")]
        public ActionResult<string>  CheckLogin()
        {
            string userName = HttpContext.User.Identity!.Name!;
            return Ok(userName);

        }

        [HttpPost("register")]
        public ActionResult<TokenPair> Register([FromBody] RegisterRequest registerRequest)
        {
            User newUser = new()
            {
                
                NickName = registerRequest.NickName,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Password = registerRequest.Password,
                Role = "user"
            };
            int? result = _userRepository.Insert(newUser);
            
            if (result == null)
                return BadRequest("User cannot be created");
            
            newUser.Id = result.Value;

            string refreshToken = Guid.NewGuid().ToString();
            
            _userRepository.SetRefreshToken(refreshToken, newUser.NickName);

            string authToken = _jwtCreator.Create(newUser.Role, newUser.Id);
            
            return new TokenPair(authToken, refreshToken);
        }

        [HttpGet("refresh/{refreshToken}")]
        public ActionResult<TokenPair> Refresh([FromRoute] string refreshToken)
        {
            var users = _userRepository.Get();

            var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            
            if(user == null)
                return Unauthorized("Refresh token is incorrect");
            
            string newRefreshToken = Guid.NewGuid().ToString();
            
            _userRepository.SetRefreshToken(newRefreshToken, user.NickName);

            string authToken = _jwtCreator.Create(user.Role, user.Id);
            
            return new TokenPair(authToken, newRefreshToken);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _userRepository.Get().ToList();
        }
        
        [Authorize(Roles = "admin")]
        [HttpDelete("{userId:int}")]
        public ActionResult DeleteUser([FromRoute] int userId)
        {
            if (_userRepository.Delete(userId))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}