using System.Text;
using BooksManager.API.Extensions;
using BooksManager.Application.Commands.Books;
using BooksManager.Application.Validators;
using BooksManager.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureMvc(builder);
ConfigureSwagger(builder);
ConfigureServices(builder);
ConfigureAuthentication(builder);
builder.Services.AddInfraestructure();

#region MVC

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}

#endregion

#region Swagger

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "BooksManager.API",
            Version = "v1",
            Description =
                "API for manage a library. Developed using the ASP.NET Core 8 framework and the Microsoft SQL Server database, using Entity Framework as ORM.",
            Contact = new OpenApiContact
            {
                Name = "Caio Silva",
                Email = "caio_tito@hotmail.com",
                Url = new Uri("https://github.com/CaioTito")
            }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using Bearer."
        });

        c.CustomSchemaIds(x => x.FullName);

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });

        var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
        var applicationName = PlatformServices.Default.Application.ApplicationName;
        var xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

        if (File.Exists(xmlDocumentPath))
        {
            c.IncludeXmlComments(xmlDocumentPath);
        }
    });
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

#region Authentication
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
