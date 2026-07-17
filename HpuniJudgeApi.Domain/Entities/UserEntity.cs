using ToDoAppApi.Domain;

namespace HpuniJudgeApi.Domain.Entities;

public class UserEntity
{
    /// <summary>
    /// Creating a new user
    /// Id Guid: primary key
    /// Name varchar(255): user name
    /// Email varchar(255): user email
    /// HashPassword varchar(255): user password
    /// CreatedAt datetime: user creation date
    /// UpdatedAt datetime: user last update date
    /// IsActive boolean: user status
    /// UserRole: many-to-many relationship with Role
    /// </summary>
    
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashPassword { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
}