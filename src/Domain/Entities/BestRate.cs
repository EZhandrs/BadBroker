using System.Text.Json.Serialization;

namespace BadBroker.Domain.Entities
{
    public class BestRate
    {
        [JsonPropertyName("rates")]
        public IList<Rate> Rates { get; set; } = new List<Rate>();

        [JsonPropertyName("buyDate")]
        public DateTime BuyDate { get; set; }

        [JsonPropertyName("sellDate")]
        public DateTime SellDate { get; set; }

        [JsonPropertyName("tool")]
        public string? Tool { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }
}