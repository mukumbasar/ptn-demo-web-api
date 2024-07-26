using FluentValidation;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Account;

namespace PtnDemoProjectAPI.Presentation.Validations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                     .NotEmpty().WithMessage("Email cannot be empty.")
                     .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Username)
                     .NotEmpty().WithMessage("Username cannot be empty.");
            RuleFor(x => x.Password)
                     .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
