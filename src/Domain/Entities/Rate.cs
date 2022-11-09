using System.Text.Json.Serialization;

namespace BadBroker.Domain.Entities
{
    public class Rate
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("rub")]
        public decimal RUB { get; set; }

        [JsonPropertyName("eur")]
        public decimal EUR { get; set; }

        [JsonPropertyName("gbp")]
        public decimal GBP { get; set; }

        [JsonPropertyName("jpy")]
        public decimal JPY { get; set; }
    }
}