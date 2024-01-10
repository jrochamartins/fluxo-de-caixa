using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Api.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostEntryRequest, Entry>();
            CreateMap<Entry, PostEntryResponse>();
            CreateMap<Balance, GetBalanceResponse>();
        }
    }
}
