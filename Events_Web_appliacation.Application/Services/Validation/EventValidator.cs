using Events_Web_application.Domain.Entities;
using FluentValidation;

namespace Events_Web_application.Application.Services.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Title).NotEmpty().Length(1, 200).WithMessage("Title can not be empty");
            RuleFor(x => x.Location).NotEmpty().Length(1, 300);
            RuleFor(x => x.MaxParticipants).NotEmpty().LessThan(5000);
            RuleFor(x => x.Category).NotNull();
        }
    }
}
