using DeMoAuthen.Data;
using DeMoAuthen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeMoAuthen.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signModel;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signModel, IConfiguration configuration)
        {
            _userManager = userManager;
            _signModel = signModel;
            _configuration = configuration;
        }
       public async Task<IdentityResult>ResgisterAsync(Register model)
        {
            var user = new ApplicationUser
            {
                FirtName = model.FirtName,
                Email = model.Email,
                LastName = model.LastName,
                UserName = model.Email,
            };
            return await _userManager.CreateAsync(user,model.Password);
        }

       public async Task<string> SignInAsync(SignModel model)
        {
            var rs = await _signModel.PasswordSignInAsync(model.Email, model.Password,false,false);

            if (!rs.Succeeded) {
                return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                
            };
            var authKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                );
           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
