using Application.Interfaces.Repositories;
using Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDBContext _context;
    public UserRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<List<User>> GetAll()
    {
        return await _context.users.ToListAsync();
    }
}
