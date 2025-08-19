using AbcloudzWebAPI.AbcloudsWebAPI.BusinessLayer.Services;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Context;
using AbcloudzWebAPI.AbcloudsWebAPI.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("InMemoryDbApp"));

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
