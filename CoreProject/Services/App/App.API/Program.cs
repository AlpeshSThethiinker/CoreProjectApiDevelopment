using App.Application;
using App.Infrastructure;
using App.Persistence;
using App.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services of application, infrastructure, persistence for registering 
builder.Services.AddApplicationServices(); // for registering mediatr
builder.Services.AddInfrastructureServices(builder.Configuration); //for registering services
builder.Services.AddPersistenceServices(); //for registering repositories


builder.Services.AddDbContext<AppDbContext>
    (_ => _.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
