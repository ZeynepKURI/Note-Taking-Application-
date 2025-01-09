using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;
using Persistence.Service;

namespace Persistence.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext with Npgsql (PostgreSQL) connection
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"), // Connection string is taken from the configuration
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) // Ensure the migrations are in the correct assembly
                ), ServiceLifetime.Scoped);
            services.AddHttpContextAccessor();
            services.AddScoped<INotesRepo, NotesRepo>();
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<IAuthService, AdminRepo>();
            services.AddScoped<IAuthService, UserRepo>();
            return services;
        }
    }
}
