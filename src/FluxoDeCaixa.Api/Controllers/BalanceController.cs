using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class BalanceController(
        ILogger<BalanceController> logger,
        INotifier notifier,
        IDailyBalanceRepository dailyBalanceRepository) :
        MainController(notifier)
    {
        private readonly ILogger<BalanceController> _logger = logger;
        private readonly IDailyBalanceRepository _dailyBalanceRepository = dailyBalanceRepository;

        // GET: <EntryController>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Entry>>> Get([FromQuery] GetBalanceResquest request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var list = await _dailyBalanceRepository.GetAsync(request.GetDateOnly());
            return Ok(list);
        }
    }
}
