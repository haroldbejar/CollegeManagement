using Data.DTOs;

namespace Application.Services
{
    public interface IAcountService
    {
        Task<UserDTO> DeleteAsync(int id);
        Task<UserDTO> GetUserByUserName(string userName);
        Task<UserDTO> Register(RegisterDTO registerDto);
        Task<bool> ValidateUserExist(string userName);
    }
}