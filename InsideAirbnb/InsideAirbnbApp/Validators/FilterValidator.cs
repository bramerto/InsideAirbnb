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
                .Must(BeInteger).WithMessage("Minimale prijs moet een geheel getal zijn.");

            RuleFor(f => f.maxPrice)
                .NotEmpty().WithMessage("Maximum prijs moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Maximum prijs moet een geheel getal zijn.");

            RuleFor(f => f.neighbourhood)
                .NotEmpty().WithMessage("Buurt moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Buurt moet een geheel getal zijn.");

            RuleFor(f => f.minReviewRate)
                .NotEmpty().WithMessage("Minimale Review Score moet ingevuld zijn.")
                .Must(BeInteger).WithMessage("Minimale Review Score moet een geheel getal zijn.");
        }

        public bool BeInteger(string input)
        {
            return input.All(char.IsDigit);
        }
    }
}
