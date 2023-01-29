using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserRoleService
{

    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public UserRoleService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    public async Task<Response<List<UserRoleDto>>> GetUserRoles()
    {
        try
        {
            var result = await _contex.UserRoles.ToListAsync();
            var mapped = _mapper.Map<List<UserRoleDto>>(result);
            return new Response<List<UserRoleDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<UserRoleDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<UserRoleDto>> AddUserRole(UserRoleDto role)
    {
        try
        {
            var existing =await _contex.UserRoles.Where(x => x.Id != role.Id).AsNoTracking().FirstOrDefaultAsync();
            if (existing == null)
            {
                return new Response<UserRoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "UserRole with this id already exists" });
            }
            var mapped = _mapper.Map<UserRole>(role);
            await _contex.UserRoles.AddAsync(mapped);
             _contex.SaveChanges();
            return new Response<UserRoleDto>(HttpStatusCode.Created,
                new List<string>() {"You are successfully added a new role to user"});
        }
        catch (Exception ex)
        {
            return new Response<UserRoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<UserRoleDto>> UpdateUserRole(UserRoleDto role)
    {
        try
        {
            var update =await _contex.UserRoles.Where(x => x.Id == role.Id ).AsNoTracking().FirstOrDefaultAsync();
            if (update == null) return new Response<UserRoleDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<UserRole>(role);
            _contex.UserRoles.Update(mapped);
            _contex.SaveChanges();
            return new Response<UserRoleDto>(role);
        }
        catch (Exception ex)
        {
            return  new Response<UserRoleDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteUserRole(int id)
    {
        var delete = await _contex.UserRoles.FirstAsync(x => x.Id == id);
        _contex.UserRoles.Remove(delete);
        await _contex.SaveChangesAsync();
        return new Response<string>("Deleted");

    }

}
