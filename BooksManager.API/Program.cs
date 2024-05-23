using BooksManager.API.Extensions;
using BooksManager.Application.Commands.Books;
using BooksManager.Application.Validators;
using BooksManager.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureMvc(builder);
ConfigureSwagger(builder);
ConfigureServices(builder);
builder.Services.AddInfraestructure();

#region MVC

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}

#endregion

#region Services
void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("BooksManagerCs");
    builder.Services.AddDbContext<BooksDbContext>(p => p.UseSqlServer(connectionString));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly));
    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
}

#endregion

#region Swagger

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen();
}

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
