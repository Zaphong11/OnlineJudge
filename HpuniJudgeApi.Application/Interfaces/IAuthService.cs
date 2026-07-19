using HpuniJudgeApi.Application.DTOs.Auth;

namespace HpuniJudgeApi.Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
}