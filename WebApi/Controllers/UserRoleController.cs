using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Microsoft.AspNetCore.Components.Route("[controller]")]

public class UserRoleController: ApiController
{
    private readonly UserRoleService _user;

    public UserRoleController(UserRoleService user)
    {
        _user = user;
    }


    [HttpGet("GetUserRoles")]
    public async Task<Response<List<UserRoleDto>>> GetUserRoles()
    {
        return await _user.GetUserRoles();
    }

    [HttpPost("AddUserRole")]
    public async  Task<Response<UserRoleDto>> AddUserRole(UserRoleDto role)
    {
        if (ModelState.IsValid)
        {
            return await _user.AddUserRole(role);
        }
        else
        {
            return new Response<UserRoleDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
       
    }

    [HttpPut("UpdateUserRole")]
    public async Task<Response<UserRoleDto>> UpdateUserRole(UserRoleDto role)
    {
        if (ModelState.IsValid)
        {
            return await _user.UpdateUserRole(role);
        }
        else
        {
            return new Response<UserRoleDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }
    
    [HttpDelete("DeleteUserRole")]
    public async Task<Response<string>> Delete(int id)
    {
        await _user.DeleteUserRole(id);
        return new Response<string>("Deleeeted");
    }


}