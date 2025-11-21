using Serilog;
using Microsoft.EntityFrameworkCore;
using CarMechanicWorkshop.Infrastructure.Persistence;
using CarMechanicWorkshop.Application.Interfaces.Services;
using CarMechanicWorkshop.Application.Interfaces.Repositories;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Infrastructure.Repositories;
using CarMechanicWorkshop.Application.Services;
using CarMechanicWorkshop.Application.Mappers;
using CarMechanicWorkshop.Api.Middleware;
using CarMechanicWorkshop.Api.Filters;
using CarMechanicWorkshop.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CarMechanicWorkshopContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging());

// Transactional Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services
builder.Services.AddControllers(options =>
{
    // You *can* apply LoggingFilter globally:
    options.Filters.Add<LoggingFilter>();
});

// Register filter & middleware in DI
builder.Services.AddScoped<LoggingFilter>();  
builder.Services.AddTransient<ExceptionMiddleware>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ClientProfile>();
    cfg.AddProfile<JobProfile>();
});

// Repositories & Services
builder.Services.AddScoped<IRepository<Client>, ClientsRepository>();
builder.Services.AddScoped<IJobsRepository, JobsRepository>();

builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IJobsService, JobsService>();

builder.Services.AddSerilog(option =>
        option.MinimumLevel.Information()
        .WriteTo.Console());

// Add CORS policy
builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:8081").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// --- Automatically apply migrations here ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarMechanicWorkshopContext>();
    if ((await db.Database.GetPendingMigrationsAsync()).Any())
    {
        await db.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
