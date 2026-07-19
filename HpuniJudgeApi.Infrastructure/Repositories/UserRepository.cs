using HpuniJudgeApi.Application.Interfaces.Repositories;
using HpuniJudgeApi.Domain.Entities;
using HpuniJudgeApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HpuniJudgeApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}