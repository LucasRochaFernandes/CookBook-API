using CookBook.API.BackgroundServices;
using CookBook.API.Converters;
using CookBook.API.Filters;
using CookBook.API.Middlewares;
using CookBook.API.Token;
using CookBook.Application;
using CookBook.Communication;
using CookBook.Domain.Security.Tokens;
using CookBook.Infra;
using CookBook.Infra.Extensions;
using CookBook.Infra.Migrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)))
     .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.  
        Enter 'Bearer' [space] and then your token in the text input below.
        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCommunication();

builder.Services.AddScoped<ITokenProvider, HttpContextToken>();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

if (builder.Configuration.IsTestEnvironment() is false)
{
    builder.Services.AddHostedService<DeleteUserService>();
    AddGoogleAuthentication();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    if (builder.Configuration.IsTestEnvironment() is true)
    {
        return;
    }
    var connectionString = builder.Configuration.GetAppConnectionString();
    var serviceProvider = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
    DatabaseMigration.Migrate(connectionString, serviceProvider);
}

void AddGoogleAuthentication()
{
    var clientId = builder.Configuration.GetValue<string>("Settings:Google:ClientId")!;
    var clientSecret = builder.Configuration.GetValue<string>("Settings:Google:ClientSecret")!;
    builder.Services.AddAuthentication(config =>
    {
        config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = clientId;
        googleOptions.ClientSecret = clientSecret;
    });
}

public partial class Program
{
    protected Program() { }
}
