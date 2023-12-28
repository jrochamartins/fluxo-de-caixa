using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FluxoDeCaixa.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class EntriesController(ILogger<EntriesController> logger, INotifier notifier, IMapper mapper, IEntriesService entriesService) :
        MainController(notifier)
    {
        // POST <EntryController>
        [HttpPost]
        public async Task<ActionResult<PostEntryResponse>> Post([FromBody] PostEntryRequest request)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var entry = mapper.Map<Entry>(request);
            await entriesService.CreateAsync(entry);
            var response = mapper.Map<PostEntryResponse>(entry);

            return CustomResponse(HttpStatusCode.Created, response);
        }
    }
}
