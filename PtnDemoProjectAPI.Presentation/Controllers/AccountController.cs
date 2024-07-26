using Microsoft.AspNetCore.Mvc;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Account;
using PtnDemoProjectAPI.BLL.Services.Abstact;

namespace PtnDemoProjectAPI.Presentation.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Registers a user and returns a result.
        /// </summary>
        /// <param name="registerDto">The registery details</param>
        /// <returns>The result of the registery attempt.</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _accountService.RegisterAsync(registerDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Authenticates a user and returns a result.
        /// </summary>
        /// <param name="logInDto">The login details</param>
        /// <returns>The result of the login attempt.</returns>
        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LogInAsync(LogInDto logInDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.LogInAsync(logInDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
