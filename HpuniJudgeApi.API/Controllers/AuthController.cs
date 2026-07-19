using HpuniJudgeApi.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = HpuniJudgeApi.Application.DTOs.Auth.RegisterRequest;

namespace HpuniJudgeApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required");
            }

            var result = await _authService.RegisterAsync(request);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }
}