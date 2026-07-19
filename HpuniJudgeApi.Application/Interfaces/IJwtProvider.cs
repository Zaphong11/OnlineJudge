using HpuniJudgeApi.Domain.Entities;

namespace HpuniJudgeApi.Application.Interfaces;

/// <summary>
/// Providing services related to the creation and management of JSON Web Tokens (JWT).
/// </summary>

public interface IJwtProvider
{
    string GenerateToken(UserEntity user, IList<string> roles);   
}