using FruitShop.Domain.Models.DTO;
using FShop.Business.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shoping.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FruitShop.Business.User
{
    public class UserBusiness : BaseBusiness<FruitShop.Domain.Models.Business.User.User>, IUserBusiness
    {
        private readonly string JWTSerect;
        public UserBusiness(IConfiguration iConfiguration) : base(iConfiguration)
        {
            JWTSerect = iConfiguration.GetSection("JWTSerect").Value;
        }

        public async Task<UserDTO> LoginAsync(string phone, string password)
        {
            var user = await Repository.GetOneAsync(x => x.Phone == phone && x.DeletedOn == null);
            if (user != null)
            {
                if (BCryptBusiness.Verify(password, user.Password))
                {
                    //tao token
                    user = await CreateJWTTokenAsync(user);
                    var userDTO = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(user));
                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<UserDTO> RegisterAsync(string phone, string password)
        {
            var addedUser = await Repository.GetOneAsync(x => x.Phone == phone);
            if (addedUser == null)
            {
                var newUser = new FruitShop.Domain.Models.Business.User.User();
                newUser.Phone = phone;
                newUser.Password = BCryptBusiness.Hash(password);
                newUser.DeletedOn = null;
                newUser.IsActive = true;
                newUser.IsTrial = true;
                Repository.Add(newUser);
                await UnitOfWork.SaveChangesAsync();

                var userDTO = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(newUser));
                return userDTO;
            }

            return null;
        }

        private async Task<FruitShop.Domain.Models.Business.User.User> CreateJWTTokenAsync(FruitShop.Domain.Models.Business.User.User user)
        {
            if (user != null)
            {
                try
                {
                    var jwtTokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(JWTSerect);

                    var identity = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, $"{user.Phone}"),
                    new Claim(ClaimTypes.Role, user.Role ?? "")
                    });

                    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = identity,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = credentials,
                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                    user.Token = jwtTokenHandler.WriteToken(token);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return user;
        }
    }
}
