using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RoleService
{
    
     private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public RoleService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    public async Task<Response<List<RoleDto>>> GetRoles()
    {
        try
        {
            var result = await _contex.Roles.ToListAsync();
            var mapped = _mapper.Map<List<RoleDto>>(result);
            return new Response<List<RoleDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<RoleDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<RoleDto>> AddRole(RoleDto role)
    {
        try
        {
            var existing =await _contex.Roles.FirstOrDefaultAsync(x => x.Id != role.Id);
            if (existing == null)
            {
                return new Response<RoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Role with this id already exists" });
            }
            var mapped = _mapper.Map<Role>(role);
            await _contex.Roles.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            return new Response<RoleDto>(HttpStatusCode.Created,
                new List<string>() {"You are successfully added a new roel"});
        }
        catch (Exception ex)
        {
            return new Response<RoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<RoleDto>> UpdateRole(RoleDto role)
    {
        try
        {
            var update =await _contex.Roles.Where(x => x.Id == role.Id ).AsNoTracking().FirstOrDefaultAsync();
            if (update == null) return new Response<RoleDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<Role>(role);
            _contex.Roles.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<RoleDto>(role);
        }
        catch (Exception ex)
        {
            return  new Response<RoleDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteRole(int id)
    {
        var delete = await _contex.Roles.FirstAsync(x => x.Id == id);
        _contex.Roles.Remove(delete);
        await _contex.SaveChangesAsync();
        return new Response<string>("Deleted");

    }

}