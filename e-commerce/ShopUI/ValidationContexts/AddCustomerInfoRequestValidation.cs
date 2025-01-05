using Application.Cart;
using FluentValidation;

namespace ShopUI.ValidationContexts
{
    public class AddCustomerInfoRequestValidation
        : AbstractValidator<AddCustomerInformation.Request>
    {
        public AddCustomerInfoRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.PostCode).NotNull();
        }

    }
}
