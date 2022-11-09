using BadBroker.Application.Common.Models;

namespace BadBroker.Application.Common.Interfaces
{
    public interface IRateData
    {
        Task<TimeSeriesResponse> GetTimeSeries(TimeSeriesRequest request);
    }
}