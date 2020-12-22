using AutoMapper;
using EReceipt.BLL.Interface;
using EReceipt.Common.Authentication;
using EReceipt.Common.Exceptions;
using EReceipt.DAL.Context;
using EReceipt.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EReceipt.BLL.Services
{
    public class AccountService: IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AccountService(UserManager<User> userManager,
            AppDbContext dbContext,
            IOptions<JwtSettings> jwtOptions,
            IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string> SignIn(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || (!await _userManager.CheckPasswordAsync(user, password)))
            {
                throw new BusinessLogicException("Email or password is incorrect.");
            }

            var token = await GenerateToken(user);

            return token;
        }

        public async Task<string> SignUpDoctor(string userName, string email, string password, string role, Doctor model)
        {
            var user = new User
            {
                UserName = userName,
                Email = email,
                Doctor = model
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }

            await _userManager.AddToRoleAsync(user, role);

            return user.Id;
        }

        public async Task<string> SignUpPatient(string userName, string email, string password, string role, Patient model)
        {
            var user = new User
            {
                UserName = userName,
                Email = email,
                Patient = model
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }

            await _userManager.AddToRoleAsync(user, role);

            return user.Id;
        }

        public async Task<string> SignUpPharmacy(string userName, string email, string password, string role, Pharmacy model)
        {
            var user = new User
            {
                UserName = userName,
                Email = email,
                Pharmacy = model
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }

            await _userManager.AddToRoleAsync(user, role);

            return user.Id;
        }

        public async Task ChangePassword(string id, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }
        }

        private async Task<string> GenerateToken(User user)
        {
            var claims = (await _userManager.GetRolesAsync(user))
                .Select(x => new Claim(CustomClaimName.Role, x))
                .ToList();

            claims.Add(new Claim(CustomClaimName.Id, user.Id));
            claims.Add(new Claim(CustomClaimName.Name, user.UserName));

            var expires = DateTime.Now.AddDays(60);
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

    }
}
