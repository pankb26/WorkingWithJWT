using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.Helpers;
using JWTHLAPI.ModelLayer.DTO.Auth;
using JWTHLAPI.ModelLayer.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.BusinessLayer.Manager.Auth
{
    public class AuthManager
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthManager(
            IUserRepository userRepository,
            JwtTokenHelper jwtTokenHelper)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userRepository.GetByUsername(request.UserName);

            if (user == null || user.IsActive != 1)
                return null;

            bool isValidPassword = PasswordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt
            );

            if (!isValidPassword)
                return null;

            var roles = new List<string> {user.RoleName };

            var tokenResult = _jwtTokenHelper.GenerateToken(
                user.Id,
                user.UserName,
                user.RoleId,
                user.RoleName
            //roles
            //  user.RoleId
            );

            return new LoginResponse
            {
                Token = tokenResult.Token,
                ExpiresAt = tokenResult.ExpireAt,
                RoleId = user.RoleId,
                RoleName = user.RoleName
            };
        }
        //public async Task<bool> Register(RegisterRequestDto request, int createdBy)
        //{
        //    var exists = await _userRepository.IsUsernameExists(request.Username);
        //    if (exists)
        //        return false;

        //    PasswordHasher.CreatePasswordHash(
        //        request.Password,
        //        out string passwordHash,
        //        out string passwordSalt
        //    );

        //    var user = new User
        //    {
        //        UserName = request.Username,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt,
        //        RoleId = request.RoleId,
        //        CreatedBy = createdBy
        //    };

        //    var userId = await _userRepository.Create(user);

        //    return userId > 0;
        //}
        public async Task<bool> SelfRegister(RegisterRequestDto request)
        {
            var exists = await _userRepository.IsUsernameExists(request.Username);
            if (exists) return false;

            var roleId = await _userRepository.GetDefaultRoleId();
            if (roleId <= 0)
                throw new Exception("Default role not configured");

            PasswordHasher.CreatePasswordHash(
                request.Password,
                out string passwordHash,
                out string passwordSalt
            );

            var user = new User
            {
                UserName = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = roleId
            };

            return await _userRepository.Create(user) > 0;
        }
        public async Task<bool> AdminRegister(AdminRegisterRequestDto request, int adminId)
        {
            var exists = await _userRepository.IsUsernameExists(request.Username);
            if (exists) return false;

            PasswordHasher.CreatePasswordHash(
                request.Password,
                out string passwordHash,
                out string passwordSalt
            );

            var user = new User
            {
                UserName = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = request.RoleId,
                CreatedBy = adminId
            };

            return await _userRepository.Create(user) > 0;
        }
    }
}