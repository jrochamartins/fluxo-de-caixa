using AutoMapper;
using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PostEntryRequest, Entry>();
            CreateMap<Entry, PostEntryResponse>();
            CreateMap<Balance, GetBalanceResponse>();
        }
    }
}
