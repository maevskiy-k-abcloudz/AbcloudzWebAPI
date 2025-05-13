using AbcloudzWebAPI.Application.Services;
using AbcloudzWebAPI.Contracts.Interfaces;
using AbcloudzWebAPI.DB;
using AbcloudzWebAPI.Domain.Interfaces;
using AbcloudzWebAPI.Infrastructure.MIddlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionMiddleware(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
