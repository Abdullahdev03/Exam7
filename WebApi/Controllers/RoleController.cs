using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Microsoft.AspNetCore.Components.Route("[controller]")]

public class RoleController : ApiController
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }


    [HttpGet("GetRoles")]
    public async Task<Response<List<RoleDto>>> GetRoles()
    {
        return await _roleService.GetRoles();
    }

    [HttpPost("AddRole")]
    public async  Task<Response<RoleDto>> AddRole(RoleDto role)
    {
        if (ModelState.IsValid)
        {
            return await _roleService.AddRole(role);
        }
        else
        {
            return new Response<RoleDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
       
    }

    [HttpPut("UpdateRole")]
    public async Task<Response<RoleDto>> UpdateRole(RoleDto role)
    {
        if (ModelState.IsValid)
        {
            return await _roleService.UpdateRole(role);
        }
        else
        {
            return new Response<RoleDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }
    
    [HttpDelete("DeleteRole")]
    public async Task<Response<string>> Delete(int id)
    {
        await _roleService.DeleteRole(id);
        return new Response<string>("Deleeeted");
    }

}