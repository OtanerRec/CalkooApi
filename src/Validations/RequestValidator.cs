using CalkooApi.DTOS;
using FluentValidation;

namespace CalkooApi.Validations
{
    public class RequestValidator : AbstractValidator<PurchaseRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Amount)
                .NotNull().WithMessage("Amount Shoud not be null.")
                .NotEmpty().WithMessage("Amount must have a value.")
                .GreaterThan(0).WithMessage("Invalid amount.")
                .ScalePrecision(2, 5);

            List<decimal> conditions = new List<decimal> { 10, 13, 20 };

            RuleFor(x => x.Rate)
                .NotEmpty().WithMessage("VAT Rate is required.")
                .Must(x => conditions.Contains(x))
                .WithMessage("Invalid VAT Rate. Please insert one of these 3 rates: " + String.Join(',', conditions));
        }
    }
}
