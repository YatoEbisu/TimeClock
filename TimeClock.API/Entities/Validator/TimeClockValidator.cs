using FluentValidation;

namespace TimeClock.API.Entities.Validator
{
    public class TimeClockValidator : AbstractValidator<TimeClock>
    {
        public TimeClockValidator()
        {
            RuleFor(y => y.EntryTime)
                .NotEmpty().WithMessage("the {PropertyName} field needs to be provided");

            RuleFor(y => y.ExitTime)
                .NotEmpty().WithMessage("the {PropertyName} field needs to be provided");

            RuleFor(y => y.PointDate)
                .NotEmpty().WithMessage("the {PropertyName} field needs to be provided");
        }
    }
}
