using Serilog;
using Microsoft.EntityFrameworkCore;
using CarMechanicWorkshop.API.Data;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.API.Repositories;
using CarMechanicWorkshop.API.Services;
using CarMechanicWorkshop.API.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<CarMechanicWorkshopContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging());

// Transactional Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ClientProfile));
builder.Services.AddAutoMapper(typeof(JobProfile));

// Repositories & Services
builder.Services.AddScoped<IRepository<ClientDatabase>, ClientsRepository>();
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
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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

app.MapControllers();

app.Run();
