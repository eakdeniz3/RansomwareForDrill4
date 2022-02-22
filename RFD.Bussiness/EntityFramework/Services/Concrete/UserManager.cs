
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Bussiness.EntityFramework.Services.Concrete;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.DTO;
using RFD.Entities.VM;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class UserManager : Repository<User>, IUserService
    {
        public string secret = "dddddddddddddddddd";

        public UserManager(RFDContext db) : base(db)
        {

        }

       
        public async Task<User> FindByUserNameUserAsync(string userName)
        {
            var user = await _db.Set<User>().Where(x => x.Username == userName).FirstOrDefaultAsync();
            return user;
        }


        public async Task<bool> CheckPasswordAsync(User user, string providedPassword)
        {
            bool result = false;
            if (PasswordMatches(providedPassword, user.PasswordHash))
            {
                result = true;
            }
            return await Task.FromResult(result);
        }







        public async Task CreateUserAsync(User user,string password)
        {

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password boş olamaz.");
            }

            user.PasswordHash = HashPassword(password);

            await _db.Set<User>().AddAsync(user);
           // await AddAsync(user);

        }




        public async Task<UserVM> GenerateJwtTokenAsync(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user?.UserId.ToString()),
                new Claim(ClaimTypes.Name, user?.Username),
                new Claim(ClaimTypes.Role,"Admin")
                //new Claim(ClaimTypes.Email, user?.Email)

            };



            var secretKey = secret;
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddMinutes(5)
                );
            var userVM = new UserVM();
            userVM.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userVM.Username = user.Username;
            // tokenInstance.RefreshToken = GenerateRefreshToken();
           // userVM.Expiration = DateTime.Now.AddMinutes(45);

            return userVM;

        }

        public string GenerateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = secret;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = true,
                ClockSkew =new TimeSpan(5),
                
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return Task.FromResult(principal);
        }


        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public bool PasswordMatches(string providedPassword, string passwordHash)
        {
            byte[] buffer4;
            if (passwordHash == null)
            {
                return false;
            }
            if (providedPassword == null)
            {
                throw new ArgumentNullException("providedPassword");
            }
            byte[] src = Convert.FromBase64String(passwordHash);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(providedPassword, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areSame = true;
            for (int i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
