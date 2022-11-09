namespace BadBroker.Application.Common.Models
{
    public class TimeSeriesRequest
    {
        public string Base { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<string> Symbols { get; set; }
    }
}