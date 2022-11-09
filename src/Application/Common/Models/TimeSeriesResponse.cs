using System.Text.Json.Serialization;

namespace BadBroker.Application.Common.Models
{
    public class TimeSeriesResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("timeseries")]
        public bool Timeseries { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, RateDto> Rates { get; set; }
    }
}