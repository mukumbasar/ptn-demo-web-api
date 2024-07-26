using FluentValidation;
using Microsoft.Extensions.Localization;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;

namespace PtnDemoProjectAPI.Presentation.Validations
{
    public class CreateBuildingDtoValidator : AbstractValidator<CreateBuildingDto>
    {
        public CreateBuildingDtoValidator()
        {
            RuleFor(x => x.ConstructionTime)
                .InclusiveBetween(30, 1800).WithMessage("Construction time should be minimum 30 seconds and maximum 1800 seconds.")
                .NotEmpty().WithMessage("ConstructionTime cannot be empty.");
            RuleFor(x => x.BuildingCost)
                .GreaterThan(0).WithMessage("Building cost cannot be zero or a negative number.")
                .NotEmpty().WithMessage("BuildingCost cannot be empty.");
            RuleFor(x => x.BuildingTypeId)
                .NotEmpty().WithMessage("BuildingTypeId cannot be empty.");
        }
    }
}
