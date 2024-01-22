using DeMoAuthen.Data;
using DeMoAuthen.Helpers;
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
        private readonly RoleManager<IdentityRole> _roleManage;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signModel, IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signModel = signModel;
            _configuration = configuration;
            _roleManage = roleManager;

        }
       public async Task<IdentityResult>ResgisterAsync(Register model)
        {

            var users = new ApplicationUser
            {
                FirtName = model.FirtName,
                Email = model.Email,
                LastName = model.LastName,
                UserName = model.Email,
            };
            // đăng kí người dùng
            var rs = await _userManager.CreateAsync(users, model.Password);
            if(rs.Succeeded)
            {
                // Kiểm tra và tạo vai trò nếu chưa tồn tại
                if (!await _roleManage.RoleExistsAsync(AppRole.Customer))
                {
                    await _roleManage.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                // Gán vai trò cho người dùng
                await _userManager.AddToRoleAsync(users, AppRole.Customer);
            }
            return rs;
        }

       public async Task<string> SignInAsync(SignModel model)
        {
            var users = await _userManager.FindByEmailAsync(model.Email);
            var checkPass = await _userManager.CheckPasswordAsync(users, model.Password);
            if (users == null || !checkPass)
            {
                return string.Empty;
            }
            //chứa các thông tin xác thực
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                
            };
            var userRole = await _userManager.GetRolesAsync(users);
            foreach(var role in userRole)
            {
                //authClaims.Add(new Claim(ClaimTypes.Role,role.ToString()));
                authClaims.Add(new Claim("role",role.ToString()));
            }
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
