using HpuniJudgeApi.Domain.Entities;

namespace HpuniJudgeApi.Domain;

public class UserRoleEntity
{
    /// <summary>
    /// Creating a new relationship between User and Role
    /// UserRole: many-to-many relationship with User and Role
    /// User: one-to-many relationship with User
    /// Role: one-to-many relationship with Role
    /// 1:N relationship
    /// UserId int: foreign key
    /// RoleId int: foreign key
    /// </summary>
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }
}