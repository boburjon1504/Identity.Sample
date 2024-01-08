using Identity.Application.Common.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut("verification/{token}")]
        public async ValueTask<IActionResult> VerificateAsync([FromRoute] string token)
        {
            var result = await _accountService.VerificationAsync(token);
            return Ok(result);
        }
    }
}
