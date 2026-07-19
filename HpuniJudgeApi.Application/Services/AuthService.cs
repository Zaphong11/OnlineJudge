using BCrypt.Net;
using HpuniJudgeApi.Application.DTOs.Auth;
using HpuniJudgeApi.Application.Interfaces;
using HpuniJudgeApi.Application.Interfaces.Repositories;
using HpuniJudgeApi.Domain;
using HpuniJudgeApi.Domain.Entities;

namespace HpuniJudgeApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var emailExists = await _userRepository.CheckEmailExistsAsync(request.Email);
        if (emailExists)
        {
            throw new Exception("Email already exists");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            HashPassword = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };
        
        newUser.UserRoles.Add(new UserRoleEntity
        {
            UserId = newUser.Id,
            RoleId = 3
        });

        await _userRepository.AddUserAsync(newUser);
        
        return "User created successfully";
    }
}