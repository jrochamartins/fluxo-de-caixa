using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class EntriesController(INotifier notifier, IMapper mapper, IEntriesService entriesService) : MainController(notifier)
    {
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
