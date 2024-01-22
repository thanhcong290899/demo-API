using DeMoAuthen.Models;
using Microsoft.AspNetCore.Identity;

namespace DeMoAuthen.Repository
{
    public interface IAccountRepository 
    {
        public Task<IdentityResult> ResgisterAsync(Register model);

        public Task<string> SignInAsync(SignModel model);
    }
}
