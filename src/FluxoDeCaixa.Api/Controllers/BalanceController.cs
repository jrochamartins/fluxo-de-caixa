using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class BalanceController(INotifier notifier, IBalanceRepository balanceRepository) : MainController(notifier)
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Entry>>> Get([FromQuery] GetBalanceResquest request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var list = await balanceRepository.GetAsync(request.GetDateOnly());
            return CustomResponse(result: list);
        }
    }
}
