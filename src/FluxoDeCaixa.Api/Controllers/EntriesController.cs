using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class EntriesController(
        ILogger<EntriesController> logger,
        INotifier notifier,
        IMapper mapper,
        IEntriesService entriesService) :
        MainController(notifier)
    {
        private readonly ILogger<EntriesController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IEntriesService _entriesService = entriesService;

        // POST <EntryController>
        [HttpPost]
        public async Task<ActionResult<PostEntryResponse>> Post([FromBody] PostEntryRequest request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var entry = _mapper.Map<Entry>(request);
            await _entriesService.CreateAsync(entry);
            var response = _mapper.Map<PostEntryResponse>(entry);

            return CustomResponse(HttpStatusCode.Created, response);
        }
    }
}
