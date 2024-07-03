using Events_Web_application.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application.Application.MidleWare.Validation
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
