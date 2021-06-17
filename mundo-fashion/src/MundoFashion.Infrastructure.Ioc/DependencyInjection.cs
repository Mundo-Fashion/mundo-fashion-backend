using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MundoFashion.Application.Services;
using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Notifications.Handlers;
using MundoFashion.Core.Storage;
using MundoFashion.Domain.Repositories;
using MundoFashion.Infrastructure.Data;
using MundoFashion.Infrastructure.Data.Repositories;
using MundoFashion.Infrastructure.Storage.Google;
using System;
using System.Reflection;
using System.Text;

namespace MundoFashion.Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void Resolve(IServiceCollection services, Assembly aplicacaoAssembly, IConfiguration configuration)
        {
            AdicionarNotificador(services);
            AdicionarDatabase(services, configuration);
            AdicionarMediatR(services, aplicacaoAssembly);
            AdicionarAutenticacaoJwt(services, configuration);
            AdicionarServices(services);
            AdicionarRepositories(services);
            AdicionarAutoMapper(services, aplicacaoAssembly);
            AdicionarFileStorage(services);
            AdicionarMemoryCache(services);
        }

        private static void AdicionarMemoryCache(IServiceCollection service)
        {
            service.AddMemoryCache();
        }

        private static void AdicionarFileStorage(IServiceCollection services)
        {
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
        }

        private static void AdicionarAutoMapper(IServiceCollection services, Assembly aplicacaoAssembly)
        {
            services.AddAutoMapper(new Assembly[] { aplicacaoAssembly }, serviceLifetime: ServiceLifetime.Singleton);
        }

        private static void AdicionarRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
        }

        private static void AdicionarServices(IServiceCollection services)
        {
            services.AddScoped<UsuarioServices>();
            services.AddScoped<AutenticacaoServices>();
            services.AddScoped<SolicitacaoServices>();
        }

        private static void AdicionarMediatR(IServiceCollection services, Assembly aplicacaoAssembly)
        {
            services.AddMediatR(aplicacaoAssembly);
        }

        private static void AdicionarDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MundoFashionContext>(options => options.UseNpgsql(connectionString));

        }

        private static void AdicionarAutenticacaoJwt(IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<TokenServices>();
        }


        private static void AdicionarNotificador(IServiceCollection services)
        {
            services.AddScoped<Notificador>();
            services.AddScoped<INotificationHandler<Notificacao>, NotificacaoHandler>();
        }
    }
}
