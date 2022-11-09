using FluentValidation;

namespace BadBroker.Application.Commands.GetBestRate
{
    public class GetBestRateCommandValidator : AbstractValidator<GetBestRateCommand>
    {
        public GetBestRateCommandValidator()
        {
            const string IncorrectFormatMessage = "Incorrect date format. Date must be like 2020-02-20 format.";
            const string IncorrectPeriodMessage = "Incorrect period. Period cannot exceed 2 months (60 days).";

            RuleFor(e => e.StartDate)
                .Length(10).WithMessage(IncorrectFormatMessage)
                .Must(BeAValidDate).WithMessage(IncorrectFormatMessage);

            RuleFor(e => e.EndDate)
                .Length(10).WithMessage(IncorrectFormatMessage)
                .Must(BeAValidDate).WithMessage(IncorrectFormatMessage);

            RuleFor(e => e.MoneyUsd)
                .GreaterThan(0);

            When(e => BeAValidDate(e.StartDate) && BeAValidDate(e.EndDate), () =>
            {
                RuleFor(e => e.EndDate)
                    .Must((model, endDate) => BeAValidPeriod(model.StartDate, endDate)).WithMessage(IncorrectPeriodMessage);
            });
        }

        private static bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        private static bool BeAValidPeriod(string startDate, string endDate)
        {
            if (DateTime.TryParse(startDate, out var start) && DateTime.TryParse(endDate, out var end))
            {
                var days = (end - start).Days;

                return days > 0 && days <= 60;
            }

            return false;
        }
    }
}