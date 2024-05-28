using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Infraestructure.Auth;
using BooksManager.Infraestructure.External_Services;
using BooksManager.Infraestructure.Persistence.Repositories;
using BooksManager.Infraestructure.Persistence;
using Quartz;
using System.Diagnostics.CodeAnalysis;

namespace BooksManager.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<ILendingRepository, LendingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
            services.ConfigureOptions<EmailBackgroundJobSetup>();

            return services;
        }
    }
}
