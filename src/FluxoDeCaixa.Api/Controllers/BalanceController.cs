using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BalanceController(ILogger<BalanceController> logger, INotifier notifier, IDailyBalanceRepository dailyBalanceRepository) :
        MainController(notifier)
    {
        // GET: <EntryController>
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
