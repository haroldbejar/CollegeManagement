using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class AcountService : IAcountService
    {
        private readonly IUserRepository _userRepository;
        public AcountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<UserDTO> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetUserByUserName(userName);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                Role = user.Role
            };
        }

        public async Task<UserDTO> Register(RegisterDTO registerDto)
        {
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                Role = registerDto.Role
            };

            await _userRepository.AddAsync(user);

            return new UserDTO
            {
                UserName = registerDto.UserName
            };
        }

        public async Task<bool> ValidateUserExist(string userName)
        {
            var existingUser = await _userRepository.GetUserByUserName(userName);
            if (existingUser != null) return true;

            return false;
        }
    }
}