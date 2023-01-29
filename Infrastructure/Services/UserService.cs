using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public UserService()
    {
        
    }

    public async Task<Response<List<UserDto>>> GetUsers()
    {
        try
        {
            var result = await _context.Users.ToListAsync();
            var mapped = _mapper.Map<List<UserDto>>(result);
            return new Response<List<UserDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<UserDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<UserDto>> LogIn(LogInDto login)
    {
        try
        {
            var existing =await  _context.Users.FirstOrDefaultAsync(x => (x.Email == login.Username || x.Phone == login.Username) && x.Password == login.Password);
            if (existing == null)
            {
                return new Response<UserDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Username or password is incorrect" });
            }
            var mapped = _mapper.Map<LogInDto>(existing);
            return new Response<UserDto>(HttpStatusCode.Accepted,
                new List<string>() { "You are welcome" });
        }
        catch (Exception ex)
        {
            return  new Response<UserDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    public async Task<Response<UserDto>> Register(RegisterDto register)
    {
        try
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Email == register.Email || x.Phone == register.Phone);
            if (existing != null)
            {
                return new Response<UserDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "This email or phone already exists" });
            }
            var mapped = _mapper.Map<User>(register);
            await _context.Users.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<UserDto>(HttpStatusCode.Created,
                new List<string>() {"You are successfully registered"});
            
            
        }
        catch (Exception ex)
        {
            return new Response<UserDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<UserDto>> UpdateUser(UserDto user)
    {
        try
        {
            var update = _context.Users.Where(x => x.UserId == user.UserId );
            if(update == null) return new Response<UserDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<User>(user);
            _context.Users.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<UserDto>(user);
        }
        catch (Exception ex)
        {
            return  new Response<UserDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        var delete = await _context.Users.FirstAsync(x => x.UserId == id);
        _context.Users.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");

    }
    
    
}
