using BlazorRPG.Server.Data;
using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _iconfiguration;
        public AuthService(DataContext dataContext, IConfiguration iconfiguration)
        {
            _dataContext = dataContext;
            _iconfiguration = iconfiguration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {

            //string response = null;
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Data = "";
                response.Success = false;
                response.Message = "User inexistent";

            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Data = user.Name;
                response.Success = false;
                response.Message = "Parola gresita";
            }
            else
            {
                //response.Message = user.Id.ToString();
                response.Data = CreateToken(user);
                response.Message = "user logat";
                response.Success = true;
            }
            return response;
        }

            public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (!await UserExists(user.Name))
            {
                CreatePasswordHash(password, out byte[] passwordhash, out byte[] passwordsalt);
                user.PasswordHash = passwordhash;
                user.PasswordSalt = passwordsalt;

                await _dataContext.AddAsync(user);
                await _dataContext.SaveChangesAsync();
                response.Data = user.Id;
                response.Message = "user inregistrat";
                response.Success = true;
                return response;

            }
            response.Message = "user existent";
            response.Success = false;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _dataContext.Users.AnyAsync(x => x.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordhash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _iconfiguration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
