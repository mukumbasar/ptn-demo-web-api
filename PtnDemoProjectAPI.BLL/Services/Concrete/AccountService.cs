using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PtnDemoProjectAPI.BLL.Configurations;
using PtnDemoProjectAPI.BLL.Dtos.Concrete;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Account;
using PtnDemoProjectAPI.BLL.HelperServices;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using PtnDemoProjectAPI.BLL.Results.Concrete;
using PtnDemoProjectAPI.BLL.Services.Abstact;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using PtnDemoProjectAPI.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenOptions = PtnDemoProjectAPI.BLL.Configurations.TokenOptions;

namespace PtnDemoProjectAPI.BLL.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenOptions _tokenOptions;

        public AccountService(UserManager<AppUser> userManager, IOptions<TokenOptions> tokenOptions)
        {
            _userManager = userManager;
            _tokenOptions = tokenOptions.Value;
        }

        /// <summary>
        /// Authenticates a user and returns a result.
        /// </summary>
        /// <param name="logInDto">The login details</param>
        /// <returns>The result of the login attempt.</returns>
        public async Task<IResult> LogInAsync(LogInDto loginDto)
        {
            var appUser = await _userManager.FindByNameAsync(loginDto.Username);

            if (appUser == null)
                return new ErrorResult("Please check your username and password.");

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(appUser, loginDto.Password);

            if(!isPasswordCorrect)
                return new ErrorResult("Please check your username and password.");

            var token = await GenerateAccessTokenAsync(appUser);

            return new SuccessDataResult<JwtDto>("Login Success" ,new JwtDto { Jwt = token });
        }

        /// <summary>
        /// Registers a user and returns a result.
        /// </summary>
        /// <param name="registerDto">The registery details</param>
        /// <returns>The result of the registery attempt.</returns>
        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            var usernameExist = await _userManager.FindByNameAsync(registerDto.Username);
            var emailExist = await _userManager.FindByEmailAsync(registerDto.Username);

            if (usernameExist != null)
                return new ErrorResult("Username already exists.");

            if (emailExist != null)
                return new ErrorResult("Email already exists.");

            AppUser appUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerDto.Email,
                UserName = registerDto.Username,
                NormalizedUserName = registerDto.Username.ToUpperInvariant()
            };

            var accountResult = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!accountResult.Succeeded)
                return new ErrorResult("Every password should have at least one uppercase letter, one lowercase letter and one special character.");

            var roleResult = await _userManager.AddToRoleAsync(appUser, Roles.AppUser.ToString());

            if (!roleResult.Succeeded) 
                return new ErrorResult("An error occured while assigning a role.");

            return new SuccessResult("Your account has been created successfully.");
        }

        /// <summary>
        /// Generates a jwt.
        /// </summary>
        /// <param name="user">The user in need of an access token</param>
        /// <returns>The result of the token generating attempt.</returns>
        private async Task<string> GenerateAccessTokenAsync(AppUser user)
        {
            DateTime notBefore = DateTime.UtcNow;
            DateTime jwtExpiration = notBefore.AddMinutes(_tokenOptions.JwtExpirationTime);

            var securityKey = JwtService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
           (issuer: _tokenOptions.Issuer,
            notBefore: notBefore,
            expires: jwtExpiration,
            claims: GetClaims(user, _tokenOptions.Audience).Result,
            signingCredentials: signingCredentials
           );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }

        /// <summary>
        /// Gets claims for a jwt.
        /// </summary>
        /// <param name="user">The user in need of their claims.</param>
        /// <param name="audiences">The audiences from the token options.</param>
        /// <returns>The claims in question.</returns>
        private async Task<IEnumerable<Claim>> GetClaims(AppUser user, List<string> audiences)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            return userList;
        }
    }
}
