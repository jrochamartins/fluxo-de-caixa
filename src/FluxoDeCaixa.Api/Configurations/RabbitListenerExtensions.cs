using FluxoDeCaixa.Domain.Abstractions.Adapters;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class RabbitListenerExtensions
    {
        private static IQueueSubscriber? _listener = null;

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            _listener = app.ApplicationServices.GetRequiredService<IQueueSubscriber>();
            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime?.ApplicationStarted.Register(OnStarted);
            lifetime?.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted() =>
            _listener?.Register();

        private static void OnStopping() =>
            _listener?.Deregister();
    }
}
