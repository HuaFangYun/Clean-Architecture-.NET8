using CA.Application.Extensions;
using CA.CrossCuttingConcerns.Constants;
using CA.Infrastructure.Extensions;
using CA.Persistence.Extensions;
using CA.WebAPI.Configurations;
using CA.WebAPI.Configurations.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connection = configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AllowCors(configuration);
builder.Services
        .AddApplicationServices()
        .AddPersistence(connection)
        .AddInfrastructure();

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = ConfigurationConstants.DefaultCacheSize;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
