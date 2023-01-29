using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile: Profile
{
    public InfrastructureProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, RegisterDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, LogInDto>();
        CreateMap<LogInDto, User>();
        CreateMap<Permission, PermissionDto>();
        CreateMap<PermissionDto, Permission>();
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();
        CreateMap<UserRole, UserRoleDto>();
        CreateMap<UserRoleDto, UserRole>();
    }
}