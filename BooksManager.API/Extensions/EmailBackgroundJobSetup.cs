using BooksManager.Infraestructure.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace BooksManager.API.Extensions
{
    public class EmailBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var emailJobKey = JobKey.Create(nameof(EmailBackgroundJob));
            options
                .AddJob<EmailBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(emailJobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(emailJobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInMinutes(5).RepeatForever()));
                        //.WithCronSchedule("0 0 8 ? * * *"));
        }
    }
}
