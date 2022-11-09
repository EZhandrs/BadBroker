using BadBroker.Domain.Entities;

namespace BadBroker.Application.Calculator
{
    public interface IBestRateCalculator
    {
        BestRate Calculate(IList<Rate> rates, decimal moneyUsd);
    }
}