using AutoMapper;
using Identity.Application.Common.Identity.Models;
using Identity.Application.Common.Identity.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IAccountService accountService, IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAll() =>
            Ok(await _userService.Get(asNoTracking: true, cancellationToken: HttpContext.RequestAborted));

        [HttpGet("{id:guid}")]
        public async ValueTask<IActionResult> Get([FromRoute] Guid id) =>
            Ok(await _userService.GetByIdAsync(id, true, HttpContext.RequestAborted));

        [HttpPost]
        public async ValueTask<IActionResult> Create([FromBody] RegisterDetails registerDetails) =>
            Ok(await _accountService.CreateUserAsync(_mapper.Map<User>(registerDetails)));

        [HttpPut]
        public async ValueTask<IActionResult> Update(Guid id, [FromBody] RegisterDetails registerDetails)
        {
            var found = await _userService.GetByIdAsync(id, true);
            var mapUser = _mapper.Map(registerDetails, found)!;

            return Ok(await _userService.UpdateAsync(mapUser, cancellationToken: HttpContext.RequestAborted));
        }

        [HttpDelete]
        public async ValueTask<IActionResult> Delete(User user) =>
            Ok(await _userService.DeleteAsync(user, cancellationToken: HttpContext.RequestAborted));
    }
}
