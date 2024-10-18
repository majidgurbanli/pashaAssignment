using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.Infrastucture.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PashaVacancyProject.Logic.FLogic
{
    public class UserBusinessLogic : BaseApplicationLogic
    {
        private readonly PasswordHasher<AdminUser> _passwordHasher;
        public UserBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _passwordHasher = new PasswordHasher<AdminUser>();
        }

        public ApplicationLogicResult RegisterUser(string username, string password)
        {
            AdminUser user = new AdminUser()
            {
                Username = username
            };
            user.Password = _passwordHasher.HashPassword(user, password);
            UnitOfWork.Repository<AdminUser>().Add(user);
            UnitOfWork.SaveChanges();
            return LogicResult(true, null);
            // Save the user to the database
        }

        public ApplicationLogicResult LoginUser(string username, string password)
        {
            AdminUser user = new AdminUser()
            {
                Username = username
            };
            user.Password = _passwordHasher.HashPassword(user, password);
            UnitOfWork.Repository<AdminUser>().Add(user);
            UnitOfWork.SaveChanges();
            return LogicResult(true, null);
            // Save the user to the database
        }
        public ApplicationLogicResult ValidateUser(string username , string password)
        {
            var user = UnitOfWork.Repository<AdminUser>().Find(x => x.Username == username).FirstOrDefault();
            if(user == null)
            {
                return new ApplicationLogicResult(false, null, "Username ve ya parol yanlisdir!");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if(result == PasswordVerificationResult.Success)
            {
                var token = GenerateToken(user);
                return LogicResult(true, token);
            }
            return LogicResult(false, null,"Ümümi xəta") ;
        }

        private string GenerateToken(AdminUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a9380f3df9592842368e73e0bf39c5e3e9ef8f6b76992745f3d998b088ef360f9f85d313d61ab97579006c3bbd1855063ca2fd550251cf730368c5989ce8865fe73a6ff648bce3bfe0a8d62f0bfbfc01fc7a5b065ea0de0f11f1cc744efd67f587d8a510c84371dedb8eb29be83c68d94b683eb7db21e115994bf8e940a13bb3fc96dab729f67b04b30a481ac84353c69687f8643af2e8b352077f6971740423eda99707adaec1f593d9d1f4804fdc717c88b341acf95b6354353eb411429569295356cc8455ba928d2faad5d1206bdc8b1a7010adb41118abd062da210ce8a51d25e61b6d3ebc39a2cc40168edad0b4ab2ae124c0a80d72103daae7171a88c9"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "test",
                audience: "test",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
