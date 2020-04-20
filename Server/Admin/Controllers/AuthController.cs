using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Databases.Sql;
using Service.Databases.Sql.Models;
using Service.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Authorize]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IServerUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(
            IServerUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var authToken = Request.Headers["Authorization"].ToString();
                var tokenString = authToken.Substring(7);

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadToken(tokenString);
                var userId = ((JwtSecurityToken)token).Payload["nameid"].ToString();

                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == userId);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                return Payloader.Success(_mapper.Map<ManagerViewModel>(entity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]ManagerViewModel body)
        {
            try
            {
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == body.Id && u.Password == body.Password);
                if (entity == null)
                    return Payloader.Fail(PayloadCode.DbNull);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,entity.Id.ToString())
                }),
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Payloader.Success(new
                {
                    manager = _mapper.Map<ManagerViewModel>(entity),
                    token = tokenString,
                });
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]ManagerViewModel body)
        {
            try
            {
                var entity = await _unitOfWork.Managers.GetAsync(u => u.Id == body.Id);
                if (entity != null)
                    return Payloader.Fail(PayloadCode.Duplication);

                var newEntity = new Manager
                {
                    Id = body.Id,
                    Password = body.Password,
                };

                await _unitOfWork.Managers.AddAsync(newEntity);
                await _unitOfWork.CommitAsync();

                return Payloader.Success(_mapper.Map<ManagerViewModel>(newEntity));
            }
            catch (Exception ex)
            {
                return Payloader.Error(ex);
            }
        }
    }
}
