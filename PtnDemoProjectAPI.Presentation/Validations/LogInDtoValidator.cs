using FluentValidation;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Account;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;

namespace PtnDemoProjectAPI.Presentation.Validations
{
    public class LogInDtoValidator : AbstractValidator<LogInDto>
    {
        public LogInDtoValidator()
        {
            RuleFor(x => x.Username)
                     .NotEmpty().WithMessage("Username cannot be empty.");
            RuleFor(x => x.Password)
                     .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
