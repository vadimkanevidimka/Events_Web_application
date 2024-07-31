using Events_Web_application.Domain.Entities;
using FluentValidation;

namespace Events_Web_application.Application.Services.Validation
{
    public class ParticipantValidator : AbstractValidator<Participant>
    {
        public ParticipantValidator() 
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(1, 200);
            RuleFor(x => x.LastName).NotEmpty().Length(1, 200);
        }
    }
}
