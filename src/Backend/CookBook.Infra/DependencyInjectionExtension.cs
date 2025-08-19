using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Domain.Security.Tokens;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.MailSender;
using CookBook.Domain.Services.MailSender.Models;
using CookBook.Domain.Services.OpenAI;
using CookBook.Domain.Services.ServiceBus;
using CookBook.Domain.Services.Storage;
using CookBook.Domain.ValueObjects;
using CookBook.Infra.Extensions;
using CookBook.Infra.Repositories;
using CookBook.Infra.Security.Cryptography;
using CookBook.Infra.Security.Tokens.Access.Generator;
using CookBook.Infra.Security.Tokens.Access.Validator;
using CookBook.Infra.Security.Tokens.Refresh;
using CookBook.Infra.Services.LoggedUser;
using CookBook.Infra.Services.MailSender;
using CookBook.Infra.Services.OpenAI;
using CookBook.Infra.Services.ServiceBus;
using CookBook.Infra.Services.Storage;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;
using System.Reflection;

namespace CookBook.Infra;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, BCryptNet>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<ICodeToPerformActionRepository, CodeToPerformActionRepository>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        AddTokens(services, configuration);
        AddOpenAI(services, configuration);
        AddAzureStorage(services, configuration);
        AddQueue(services, configuration);
        AddMailSender(services, configuration);
        if (configuration.IsTestEnvironment() is true)
        {
            return;
        }
        services.AddDbContext<AppDbContext>();
        AddFluentMigrator(services, configuration);
    }
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetAppConnectionString();
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("CookBook.Infra")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(signingKey!, expirationTimeMinutes));
        services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));

    }
    private static void AddOpenAI(IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration.GetValue<string>("Settings:OpenAI:ApiKey");
        if (string.IsNullOrEmpty(apiKey) is false)
        {
            services.AddScoped<IGenerateRecipeAI, ChatGPTService>();
            services.AddScoped(c => new ChatClient(AppRuleConstants.CHAT_MODEL, apiKey));
        }
    }
    private static void AddAzureStorage(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Settings:BlobStorage:Azure");
        if (string.IsNullOrEmpty(connectionString) is false)
        {
            services.AddScoped<IBlobStorageService>(c => new AzureStorageService(new BlobServiceClient(connectionString)));
        }
    }
    private static void AddQueue(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Settings:ServiceBus:DeleteUserAccount");

        if (string.IsNullOrEmpty(connectionString) is true)
            return;

        var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        });

        string queueName = "user";
        var deleteQueue = new DeleteUserQueue(client.CreateSender(queueName));

        var deleteUserProcessor = new DeleteUserProcessor(client.CreateProcessor(queueName, new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1
        }));
        services.AddSingleton(deleteUserProcessor);

        services.AddScoped<IDeleteUserQueue>(opt => deleteQueue);
    }
    private static void AddMailSender(IServiceCollection services, IConfiguration configuration)
    {
        var mailSettings = configuration.GetSection("Settings:MailSender").Get<MailSettings>();
        if (mailSettings is null)
            return;
        services.AddScoped<IMailSenderService>(opt => new MailSenderService(mailSettings));
    }
}
