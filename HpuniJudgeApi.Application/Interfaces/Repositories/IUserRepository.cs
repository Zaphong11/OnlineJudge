using HpuniJudgeApi.Domain.Entities;

namespace HpuniJudgeApi.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> CheckEmailExistsAsync(string email);
    Task AddUserAsync(UserEntity user);
}