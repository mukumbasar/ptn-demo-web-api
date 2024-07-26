using PtnDemoProjectAPI.BLL.Dtos.Concrete.Account;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Abstact
{
    public interface IAccountService
    {
        /// <summary>
        /// Authenticates a user and returns a result.
        /// </summary>
        /// <param name="logInDto">The login details</param>
        /// <returns>The result of the login attempt.</returns>
        Task<IResult> LogInAsync(LogInDto logInDto);

        /// <summary>
        /// Registers a user and returns a result.
        /// </summary>
        /// <param name="registerDto">The registery details</param>
        /// <returns>The result of the registery attempt.</returns>
        Task<IResult> RegisterAsync(RegisterDto registerDto);
    }
}
