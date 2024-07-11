using Events_Web_application.Domain.Models;
using FluentValidation;

namespace Events_Web_application.Application.MidleWare.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Title).NotEmpty().Length(0, 200);
            RuleFor(x => x.Description).NotEmpty().Length(0, 1000);
            RuleFor(x => x.Location).NotEmpty().Length(0, 300);
            RuleFor(x => x.Category).NotNull();
        }
    }
}
