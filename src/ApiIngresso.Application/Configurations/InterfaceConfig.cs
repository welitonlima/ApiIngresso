using ApiIngresso.Application.Interfaces;
using ApiIngresso.Application.Notification;
using ApiIngresso.Application.Services;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIngresso.Application.Configurations
{
    public static class InterfaceConfig
    {
        public static IServiceCollection ResolveInterfaces(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();

            //services
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            //repository
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            return services;
        }
    }
}
