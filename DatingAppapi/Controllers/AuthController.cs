using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingAppapi.Data;
using DatingAppapi.Dtos;
using DatingAppapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;



namespace DatingAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.config = config;
            this.repo = repo;

        }

        [HttpPost("register")]

        public async Task<IActionResult> register(UserForRegisterDto dto)
        {
            dto.userName = dto.userName.ToLower();
            if (await this.repo.UserExist(dto.userName)) return BadRequest();

            var userToCreate = new User
            {
                userName = dto.userName
            };

            var userCreate = await this.repo.Register(userToCreate, dto.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserForLoginDto dto)
        {
            var userFromRepo = await this.repo.Login(dto.userName, dto.password);
            if (userFromRepo == null) return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.userName)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(this.config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new{
                token=tokenHandler.WriteToken(token)
            });

             
        }
    }
}