
using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PermissionService
{
    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public PermissionService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    public async Task<Response<List<PermissionDto>>> GetPermissions()
    {
        try
        {
            var result = await _contex.Permissions.ToListAsync();
            var mapped = _mapper.Map<List<PermissionDto>>(result);
            return new Response<List<PermissionDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<PermissionDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<PermissionDto>> AddPermission(PermissionDto model)
    {
        try
        {
            var existing = await _contex.Permissions.FirstOrDefaultAsync(x => x.PermissionId != model.PermissionId);
            if (existing != null)
            {
                return new Response<PermissionDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Permission with this id already exists" });
            }
            var mapped = _mapper.Map<Permission>(model);
            await _contex.Permissions.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            return new Response<PermissionDto>(HttpStatusCode.Created,
                new List<string>() {"You are successfully added a new roel"});
        }
        catch (Exception ex)
        {
            return new Response<PermissionDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<PermissionDto>> UpdatePermission(PermissionDto model)
    {
        try
        {
            var update =await _contex.Permissions.Where(x => x.PermissionId == model.PermissionId ).AsNoTracking().FirstOrDefaultAsync();
            if (update == null) return new Response<PermissionDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<Permission>(model);
            _contex.Permissions.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<PermissionDto>(model);
        }
        catch (Exception ex)
        {
            return  new Response<PermissionDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeletePermission(int id)
    {
        var delete = await _contex.Permissions.FirstAsync(x => x.PermissionId == id);
        _contex.Permissions.Remove(delete);
        await _contex.SaveChangesAsync();
        return new Response<string>("Deleted");

    }

}
