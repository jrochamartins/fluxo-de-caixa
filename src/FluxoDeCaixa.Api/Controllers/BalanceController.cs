using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class BalanceController(INotifier notifier, IDailyBalanceRepository dailyBalanceRepository) :
        MainController(notifier)
    {
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Entry>>> Get([FromQuery] GetBalanceResquest request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var list = await dailyBalanceRepository.GetAsync(request.GetDateOnly());
            return CustomResponse(result: list);
        }
    }
}
