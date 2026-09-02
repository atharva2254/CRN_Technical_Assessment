using CRN_Technical_Assessment.Application.DTOs;
using FluentValidation;

namespace CRN_Technical_Assessment.Data.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
        {
            public ProductValidator()
            {
                RuleFor(x => x.ProductName)
                    .NotEmpty()
                    .WithMessage("Product name is required.")

                    .MaximumLength(255)
                    .WithMessage("Product name cannot exceed 255 characters.");
            }
    }
}
