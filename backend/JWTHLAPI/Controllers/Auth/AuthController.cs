using JWTHLAPI.BusinessLayer.Manager.Auth;
using JWTHLAPI.ModelLayer.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTHLAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthManager _authManager;

        public AuthController(AuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null)
                    return Ok(new
                    {
                        Success = false,
                        Message = "Invalid request",
                        data = (object)null
                    });

                var result = await _authManager.Login(request);

                if (result == null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "Invalid username or password",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Login successful",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Success = false,
                    Message = ex.Message,
                    data = (object)null
                });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _authManager.Register(request, adminId);

            if (!result)
                return BadRequest("Username already exists");

            return Ok("User registered successfully");
        }
    }
}