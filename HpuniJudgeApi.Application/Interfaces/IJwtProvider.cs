using HpuniJudgeApi.Domain.Entities;

namespace ToDoAppApi.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user, IList<string> roles);   
}