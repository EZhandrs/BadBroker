using Microsoft.AspNetCore.Mvc;

using BadBroker.Application.Commands;
using BadBroker.Application.Commands.GetBestRate;
using BadBroker.Domain.Entities;

namespace BadBroker.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly ICommandHandler<GetBestRateCommand, BestRate> _handler;

        public RatesController(ILogger<RatesController> logger, ICommandHandler<GetBestRateCommand, BestRate> handler)
        {
            _logger = logger;
            _handler = handler;
        }

        [HttpGet("best")]
        public async Task<IActionResult> Best([FromQuery] GetBestRateCommand request)
        {
            _logger.LogInformation("{StartDate} - {EndDate} {MoneyUsd}$", request.StartDate, request.EndDate, request.MoneyUsd);

            var result = await _handler.HandleAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}