using Data.Entities;

namespace Infrastructure.Repositoties
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetUserByUserName(string userName);
    }
}