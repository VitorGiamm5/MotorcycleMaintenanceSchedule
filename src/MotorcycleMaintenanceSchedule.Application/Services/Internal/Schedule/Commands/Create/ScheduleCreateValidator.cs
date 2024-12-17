using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public class ScheduleCreateValidator : AbstractValidator<ScheduleCreateCommand>
{
    public ScheduleCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be at most 100 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be valid.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .GreaterThan(0).WithMessage("Phone must be a positive number.")
            .When(x => x.Phone.HasValue);

        RuleFor(x => x.PhoneDDD)
            .InclusiveBetween(10, 99).WithMessage("DDD must be between 10 and 99.")
            .When(x => x.PhoneDDD.HasValue);

        RuleFor(x => x.Observation)
            .MaximumLength(500).WithMessage("Observation must be at most 500 characters long.")
            .When(x => !string.IsNullOrEmpty(x.Observation));

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be a valid value.");

        RuleFor(x => x.MotorcycleId)
            .NotEmpty().WithMessage("Motorcycle ID is required.");
    }
}
