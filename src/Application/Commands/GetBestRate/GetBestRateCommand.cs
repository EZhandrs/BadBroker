using Microsoft.Extensions.Logging;

using BadBroker.Domain.Entities;
using BadBroker.Application.Common.Interfaces;
using BadBroker.Application.Common.Models;
using BadBroker.Application.Calculator;
using BadBroker.Domain.Constants;

namespace BadBroker.Application.Commands.GetBestRate
{
    public class GetBestRateCommand
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal MoneyUsd { get; set; }
    }

    public class GetBestRateCommandHandler : ICommandHandler<GetBestRateCommand, BestRate>
    {
        private readonly ILogger _logger;
        private readonly IRateData _rateData;
        private readonly IBestRateCalculator _calculator;

        public GetBestRateCommandHandler(ILogger<GetBestRateCommandHandler> logger, IRateData rateData, IBestRateCalculator calculator)
        {
            _logger = logger;
            _rateData = rateData;
            _calculator = calculator;
        }

        public async Task<Result<BestRate>> HandleAsync(GetBestRateCommand request)
        {
            var dataApiRequest = new TimeSeriesRequest
            {
                Base = Currency.USD,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Symbols = new[] { Currency.RUB, Currency.EUR, Currency.GBP, Currency.JPY },
            };

            var result = new Result<BestRate>();
            TimeSeriesResponse timeSeriesResponse = null;

            try
            {
                timeSeriesResponse = await _rateData.GetTimeSeries(dataApiRequest);
                result.IsSuccess = true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                result.ErrorMessage = "Problem with rates api";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.ErrorMessage = "Unknown problem with rates api";
            }

            if (!result.IsSuccess) return result;

            var rates = MapRates(timeSeriesResponse.Rates);
            result.Value = _calculator.Calculate(rates, request.MoneyUsd);

            return result;
        }

        private static IList<Rate> MapRates(Dictionary<string, RateDto> dtoRates)
        {
            var rates = new List<Rate>(dtoRates.Count);

            foreach (var rate in dtoRates)
            {
                rates.Add(new Rate
                {
                    Date = DateTime.Parse(rate.Key),
                    RUB = rate.Value.RUB,
                    EUR = rate.Value.EUR,
                    GBP = rate.Value.GBP,
                    JPY = rate.Value.JPY,
                });
            }

            return rates;
        }
    }
}