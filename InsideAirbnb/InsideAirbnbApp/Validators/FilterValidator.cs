using System.Linq;
using FluentValidation;
using InsideAirbnbApp.Util;

namespace InsideAirbnbApp.Validators
{
    public class FilterValidator : AbstractValidator<FilterRequest>
    {
        public FilterValidator()
        {
            RuleFor(f => f.minPrice)
                .NotEmpty().WithMessage("Minimale prijs moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Minimale prijs moet een geheel getal zijn.")
                .GreaterThanOrEqualTo(f => "0").WithMessage("Minimale prijs moet groter dan 0 zijn.")
                .LessThan(f => f.maxPrice).WithMessage("Minimale prijs moet kleiner dan maximum prijs zijn.");

            RuleFor(f => f.maxPrice)
                .NotEmpty().WithMessage("Maximum prijs moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Maximum prijs moet een geheel getal zijn.")
                .GreaterThanOrEqualTo(f => "0").WithMessage("Maximum prijs moet groter dan 0 zijn.")
                .GreaterThan(f => f.minPrice).WithMessage("Maximum prijs moet groter dan minimale prijs zijn.");

            RuleFor(f => f.neighbourhood)
                .NotEmpty().WithMessage("Buurt moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Buurt moet een geheel getal zijn.")
                .GreaterThanOrEqualTo(f => "0").WithMessage("Buurt moet groter dan 0 zijn.");

            RuleFor(f => f.minReviewRate)
                .NotEmpty().WithMessage("Minimale Review Score moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Minimale Review Score moet een geheel getal zijn.")
                .GreaterThanOrEqualTo(f => "0").WithMessage("Minimale Review Score moet groter dan 0 zijn.");
        }

        public bool BeInteger(string input)
        {
            return input.All(char.IsDigit);
        }
    }
}
