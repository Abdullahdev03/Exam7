using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class UserLoginController : ApiController
{
    private readonly UserLogInService _service;

    public UserLoginController(UserLogInService service)
    {
        _service = service;
    }
    // [HttpGet("GetRoles")]
    // public async Task<Response<List<RoleDto>>> GetRoles()
    // {
    //     return await _roleService.GetRoles();
    // }

    [HttpPost("AddTime")]
    public async  Task<Response<UserLoginDto>> AddTime(UserLoginDto role)
    {
        if (ModelState.IsValid)
        {
            return await _service.loginTime(role);
        }
        else
        {
            return new Response<UserLoginDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
       
    }
}