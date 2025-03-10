using AbcloudzWebAPI.DataAccess;
using AbcloudzWebAPI.DataAccess.Repositories;
using AbcloudzWebAPI.Domain;
using AbcloudzWebAPI.Infrastructure.Repositories;
using AbcloudzWebAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISecurityService, SecurityService>();

builder.Services.AddDbContextPool<ApplicationContext>(
    _ => 
    _.UseInMemoryDatabase("testdb"));

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
