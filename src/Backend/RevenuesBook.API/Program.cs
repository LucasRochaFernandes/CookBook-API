using RevenuesBook.API.Filters;
using RevenuesBook.API.Middlewares;
using RevenuesBook.Application;
using RevenuesBook.Communication;
using RevenuesBook.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
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

app.Run();
