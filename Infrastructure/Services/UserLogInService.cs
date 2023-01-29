using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserLogInService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserLogInService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<UserLoginDto>> loginTime(UserLoginDto role)
    {
        try
        {
            var existing =await _context.UserLogIns.FirstOrDefaultAsync(x => x.Id != role.Id);
            if (existing == null)
            {
                return new Response<UserLoginDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Errors" });
            }
            var mapped = _mapper.Map<UserLogIn>(role);
            await _context.UserLogIns.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<UserLoginDto>(HttpStatusCode.Created,
                new List<string>() {"Time add"});
        }
        catch (Exception ex)
        {
            return new Response<UserLoginDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
}