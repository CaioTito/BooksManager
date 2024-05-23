using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Infraestructure.External_Services;
using BooksManager.Infraestructure.Persistence.Repositories;
using BooksManager.Infraestructure.Persistence;
using Quartz;

namespace BooksManager.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILendingRepository, LendingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
            services.ConfigureOptions<EmailBackgroundJobSetup>();
        }
    }
}
