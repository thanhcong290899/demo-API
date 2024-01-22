using DeMoAuthen.Models;
using DeMoAuthen.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeMoAuthen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register register)
        {
            var rs = await _repo.ResgisterAsync(register);
            if (rs.Succeeded)
            {
                return Ok(rs);
            } return Unauthorized();
        }
        [HttpPost("SignIn")]
            public async Task<IActionResult> SignIn(SignModel signModel)
        {
            var rs = await _repo.SignInAsync(signModel);
            if (string.IsNullOrEmpty(rs)) {
                return Unauthorized();
            };
            return Ok(rs);
        }
    }
}
