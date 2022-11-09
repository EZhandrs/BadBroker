using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using BadBroker.Application.Common.Interfaces;
using BadBroker.Application.Common.Models;
using BadBroker.Infrastructure.Options;

namespace BadBroker.Infrastructure.Services
{
    internal class RateDataService : IRateData
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _factory;
        private readonly ExchangeRatesDataApiOptions _options;

        public RateDataService(ILogger<RateDataService> logger, IHttpClientFactory factory, IOptions<ExchangeRatesDataApiOptions> options)
        {
            _logger = logger;
            _factory = factory;
            _options = options.Value;
        }

        public async Task<TimeSeriesResponse> GetTimeSeries(TimeSeriesRequest request)
        {
            const string urlFormat = "{0}/timeseries?base={1}&symbols={2}&start_date={3}&end_date={4}";

            var urlWithQuery = string.Format(urlFormat, _options.Url, request.Base, string.Join(',', request.Symbols), request.StartDate, request.EndDate);
            var httpClient = _factory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("apikey", _options.Key);

            _logger.LogInformation("--> Sending request to {Url}", urlWithQuery);

            var response = await httpClient.GetAsync(urlWithQuery);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TimeSeriesResponse>(content);
        }
    }
}