using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Notifications.Handlers;
using System.Reflection;

namespace MundoFashion.Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void Resolve(IServiceCollection services, Assembly aplicacaoAssembly)
        {
            AdicionarNotificador(services);
            AdicionarDatabase(services);
            AdicionarMediatR(services, aplicacaoAssembly);
        }

        private static void AdicionarMediatR(IServiceCollection services, Assembly aplicacaoAssembly)
        {
            services.AddMediatR(aplicacaoAssembly);
        }

        private static void AdicionarDatabase(IServiceCollection services)
        {
           //   throw new NotImplementedException();
        }

        private static void AdicionarNotificador(IServiceCollection services)
        {
            services.AddScoped<Notificador>();
            services.AddScoped<INotificationHandler<Notificacao>, NotificacaoHandler>();
        }
    }
}
