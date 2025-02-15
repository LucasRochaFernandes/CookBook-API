using RevenuesBook.API.Filters;
using RevenuesBook.API.Middlewares;
using RevenuesBook.Application;
using RevenuesBook.Communication;
using RevenuesBook.Infra;
using RevenuesBook.Infra.Extensions;
using RevenuesBook.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCommunication();


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

app.Run();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.GetAppConnectionString();
    var serviceProvider = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
    DatabaseMigration.Migrate(connectionString, serviceProvider);
}