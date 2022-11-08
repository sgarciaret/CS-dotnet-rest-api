using ApiRest.DTO;
using ApiRest.Model;
using ApiRest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRest.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> log;
        private readonly IUsersSQLServer repository;

        public LoginController(IConfiguration configuration, ILogger<LoginController> log, IUsersSQLServer repository)
        {
            this.configuration = configuration;
            this.log = log;
            this.repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Login(LoginAPI userLogin)
        {
            UserAPI User = null;
            User = await AuthenticateUserAsync(userLogin);
            if (User == null)
                throw new Exception("Credenciales no válidas");
            else
                User = GenerateTokenJWT(User);

            return User.convertDTO();
        } 

        private async Task<UserAPI> AuthenticateUserAsync(LoginAPI userLogin)
        {
            UserAPI uerAPI = await repository.GiveUser(userLogin);
            return uerAPI;
        }

        private UserAPI GenerateTokenJWT(UserAPI userInfo)
        {
            // Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])
                );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // Claims
            var _Claims = new[]
            {
                new Claim("user", userInfo.User),
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            // Payload
            var _Payload = new JwtPayload(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1)
                );

            // Token
            var _Token = new JwtSecurityToken(
                _Header,
                _Payload
                );
            userInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return userInfo;
        }
    }
}
