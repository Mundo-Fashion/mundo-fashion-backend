﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Notifications.Handlers;
using MundoFashion.Domain.Repositories;
using MundoFashion.Infrastructure.Data;
using MundoFashion.Infrastructure.Data.Repositories;
using System.Reflection;
using System.Text;

namespace MundoFashion.Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void Resolve(IServiceCollection services, Assembly aplicacaoAssembly, IConfiguration configuration)
        {
            AdicionarNotificador(services);
            AdicionarDatabase(services);
            AdicionarMediatR(services, aplicacaoAssembly);
            AdicionarAutenticacaoJwt(services, configuration);
            AdicionarServices(services);
            AdicionarRepositories(services);
        }

        private static void AdicionarRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void AdicionarServices(IServiceCollection services)
        {
            services.AddScoped<UsuarioServices>();
            services.AddScoped<AutenticacaoServices>();
        }

        private static void AdicionarMediatR(IServiceCollection services, Assembly aplicacaoAssembly)
        {
            services.AddMediatR(aplicacaoAssembly);
        }

        private static void AdicionarDatabase(IServiceCollection services)
        {
            services.AddDbContext<MundoFashionContext>(options => options.UseInMemoryDatabase("inmemory-database"));

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
