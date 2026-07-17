using ToDoAppApi.Domain;

namespace HpuniJudgeApi.Domain.Entities;

public class RoleEntity
{
    /// <summary>
    /// Creating a new role
    /// id int: primary key (ID: 1, 2, 3)
    /// Name varchar(255): role name (Admin, User, Guest)
    /// UserRole: many-to-many relationship with User
    /// </summary>
    
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
}