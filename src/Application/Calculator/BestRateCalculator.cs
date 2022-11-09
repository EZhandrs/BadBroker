using BadBroker.Domain.Constants;
using BadBroker.Domain.Entities;

namespace BadBroker.Application.Calculator
{
    public class BestRateCalculator : IBestRateCalculator
    {
        public BestRate Calculate(IList<Rate> rates, decimal moneyUsd)
        {
            var maxMoney = moneyUsd;
            var profitableTool = Currency.RUB;

            int maxi = 0, maxj = 0;
            for (int i = 0; i < rates.Count - 1; i++)
            {
                for (int j = i + 1; j < rates.Count; j++)
                {
                    var maxCoefficient = rates[i].RUB / rates[j].RUB;
                    var tool = Currency.RUB;

                    var coefficint = rates[i].EUR / rates[j].EUR;
                    if (maxCoefficient < coefficint)
                    {
                        tool = Currency.EUR;
                        maxCoefficient = coefficint;
                    }

                    coefficint = rates[i].GBP / rates[j].GBP;
                    if (maxCoefficient < coefficint)
                    {
                        tool = Currency.GBP;
                        maxCoefficient = coefficint;
                    }

                    coefficint = rates[i].JPY / rates[j].JPY;
                    if (maxCoefficient < coefficint)
                    {
                        tool = Currency.JPY;
                        maxCoefficient = coefficint;
                    }

                    var maxMoneyForPeriod = moneyUsd * maxCoefficient - (rates[j].Date - rates[i].Date).Days;

                    if (maxMoneyForPeriod > maxMoney)
                    {
                        maxMoney = maxMoneyForPeriod;
                        maxi = i;
                        maxj = j;
                        profitableTool = tool;
                    }
                }
            }

            return new BestRate
            {
                BuyDate = rates[maxi].Date,
                SellDate = rates[maxj].Date,
                Rates = rates,
                Revenue = maxMoney - moneyUsd,
                Tool = profitableTool
            };
        }
    }
}