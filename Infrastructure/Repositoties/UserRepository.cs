using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositoties
{
    public class UserRepository : IUserRepository
    {
        private readonly CollegeContext _context;
        public UserRepository(CollegeContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }

}