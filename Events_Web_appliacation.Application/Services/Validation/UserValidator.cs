using Events_Web_application.Domain.Entities;
using FluentValidation;

namespace Events_Web_application.Application.Services.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress().MaximumLength(50).WithMessage("Email is too long!");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password can not be less than 8 symbols");
        }
    }
}
