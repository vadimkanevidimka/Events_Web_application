using Events_Web_application.Domain.Models;
using FluentValidation;

namespace Events_Web_application.Application.MidleWare.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
