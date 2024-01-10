using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class BalanceController(INotifier notifier, IBalanceRepository balanceRepository, IMapper mapper, ILogger<BalanceController> logger) : MainController(notifier)
    {
        [HttpGet]
        public async Task<ActionResult<GetBalanceResponse>> Get([FromQuery] GetBalanceRequest request)
        {
            logger.LogInformation("HTTP GET /Balance started");

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var balance = await balanceRepository.GetByDateAsync(request.GetDateOnly());
            var response = mapper.Map<GetBalanceResponse>(balance);

            return CustomResponse(result: response);
        }
    }
}
