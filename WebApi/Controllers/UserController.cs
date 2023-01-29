using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class UserController : ApiController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    [HttpGet("GetUser")]
    public async Task<Response<List<UserDto>>> Get()
    {
        return await _userService.GetUsers();
    }
    
    [HttpPost("login")]
    public async Task<Response<UserDto>> Login(LogInDto login)
    {
        if (ModelState.IsValid)
        {
            return await _userService.LogIn(login);
        }
        else
        {
            return new Response<UserDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }
    
    [HttpPost("register")]
    public async Task<Response<UserDto>> Register(RegisterDto register)
    {
        if (ModelState.IsValid)
        {
            return await _userService.Register(register);
        }
        else
        {
            return new Response<UserDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
        
    }
    [HttpPut("UpdateUser")]
    public async Task<Response<UserDto>> UpdateUser(UserDto user)
    {
        if (ModelState.IsValid)
        {
            return await _userService.UpdateUser(user);
        }
        else
        { 
            return new Response<UserDto>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }

    [HttpDelete("DeleteUser")]
    public async Task<Response<string>> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return new Response<string>("Deleeeted");
    }
}