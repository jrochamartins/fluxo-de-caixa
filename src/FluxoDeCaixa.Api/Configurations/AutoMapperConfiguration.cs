namespace FluxoDeCaixa.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static WebApplicationBuilder AddAddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }
    }
}
