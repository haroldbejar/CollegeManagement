using System.Security.Cryptography;
using System.Text;
using Application.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    /// <summary>
    /// Acount Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AcountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAcountService _acountService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="acountService"></param>
        public AcountController(ITokenService tokenService, IAcountService acountService)
        {
            _tokenService = tokenService;
            _acountService = acountService;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="resgisterDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO resgisterDTO)
        {
            try
            {
                var existingUser = await _acountService.ValidateUserExist(resgisterDTO.UserName);
                if (existingUser) return BadRequest("El usuario ya existe!");

                await _acountService.Register(resgisterDTO);

                var user = new LogedUserDTO
                {
                    UserName = resgisterDTO.UserName,
                    Role = resgisterDTO.Role,
                    Token = _tokenService.CreateToken(resgisterDTO)
                };

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login(RegisterDTO registerUser)
        {
            var existingUser = await _acountService.GetUserByUserName(registerUser.UserName);
            if (registerUser == null) return Unauthorized();

            using var hmac = new HMACSHA512(existingUser.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != existingUser.PasswordHash[i]) return Unauthorized();
            }

            var user = new LogedUserDTO
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Role = existingUser.Role,
                Token = _tokenService.CreateToken(registerUser)
            };

            return Ok(user);
        }
    }
}