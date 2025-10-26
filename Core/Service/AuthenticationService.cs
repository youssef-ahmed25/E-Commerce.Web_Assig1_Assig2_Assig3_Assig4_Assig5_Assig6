using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModule;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService (UserManager<ApplicationUser>_userManager,IConfiguration _configuration,IMapper _mapper): IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
          var user= await  _userManager.FindByEmailAsync(email);
            return user is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            return new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };

        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            if (user.Address is null)
                throw new AddressNotFoundException(user.UserName!);

            return _mapper.Map<Address,AddressDto>(user.Address);

        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;

            }
            else
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
           await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isPasswordValid)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user)
                };
            }
            throw new UnAutherizedException();

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());
            }
        }
        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration.GetSection("JWTOption")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOption:Issuer"],
                audience: _configuration["JWTOption:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
