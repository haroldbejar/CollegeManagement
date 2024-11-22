using Data.DTOs;

namespace Application.Services
{
    public interface ITokenService
    {
        string CreateToken(RegisterDTO registerDTO);
    }
}