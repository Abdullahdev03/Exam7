using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class PermissionController: ApiController
{

    private readonly PermissionService _permissionService;

    public PermissionController(PermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet("GetPermissions")]
    public async Task<Response<List<PermissionDto>>> GetPermissions()
    {
        return await _permissionService.GetPermissions();
    }

    [HttpPost("AddPermission")]
    public async  Task<Response<PermissionDto>> AddPermission(PermissionDto model)
    {
        if (ModelState.IsValid)
        {
            return await _permissionService.AddPermission(model);
        }
        else
        {
            return new Response<PermissionDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
       
    }

    [HttpPut("UpdatePermission")]
    public async Task<Response<PermissionDto>> UpdatePermission(PermissionDto model)
    {
        if (ModelState.IsValid)
        {
            return await _permissionService.UpdatePermission(model);
        }
        else
        {
            return new Response<PermissionDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }
    
    [HttpDelete("DeletePermission")]
    public async Task<Response<string>> Delete(int id)
    {
        await _permissionService.DeletePermission(id);
        return new Response<string>("Deleeeted");
    }

}
